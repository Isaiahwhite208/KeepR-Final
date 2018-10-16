<template>
  <div v-if="activeVault">
    <v-toolbar height="60rem" app>
      <v-btn class=" logout" raised @click="logoutDialog = true">Logout</v-btn>
      <v-spacer></v-spacer>
      <v-btn @click="$router.push({ name: 'home' })" class="back-button">Back</v-btn>
    </v-toolbar>
    <div class="page-wrapper">
      <section>
        <h1 class=" heading">Currently viewing the <br /> {{activeVault.name}} vault</h1>
      </section>
      <v-divider color="#66fcf1"></v-divider>
      <section>
        <v-card>
          <v-container row wrap>
            <v-flex xs12>
              <v-card-title>
                <p class="vault-description">{{activeVault.description}}</p>
                <v-btn flat color="#66fcf1" @click="addKeepDialogVal = true" fab>Add</v-btn>
                <v-btn flat color="#66fcf1" @click="editVaultDialogVal = true" fab>Edit</v-btn>
                <v-btn flat color="#66fcf1" @click="deleteVaultDialogVal = true" fab>Delete</v-btn>
              </v-card-title>
              <p class=" keep-count">Keep Count: 0
              </p>
              <v-divider color="#66fcf1"></v-divider>
              <div class="vaultKeeps">
                <v-layout v-if="vaultKeeps.length > 0">
                  <div v-for="keep in vaultKeeps">

                    <h3>{{keep.name}}</h3>
                    <button @click="removeKeep(keep.id)">Remove from vault</button>
                  </div>
                </v-layout>
                <div v-else class="no-keeps">
                  <p>You currently don't have any keeps in this vault</p>
                </div>
              </div>
            </v-flex>
          </v-container>
        </v-card>
      </section>
      <v-dialog scrollable v-model="addKeepDialogVal" max-width="400">
        <v-card class="form">
          <v-card-title style="margin-top: .5rem;">Select a keep to add to vault:</v-card-title>
          <v-divider style="margin: 1rem;" color="#66fcf1"></v-divider>
          <v-card-text style="height: 400px;">
            <v-radio-group column>
              <div v-for="keep in userKeeps">
                <h4>{{keep.name}}</h4>
                <v-btn @click="addKeep(keep.id)">Add</v-btn>
              </div>
            </v-radio-group>
            <v-divider color="#66fcf1"></v-divider>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="addKeepDialogVal = false">Cancel</v-btn>
            <v-btn @click="addKeep(keep.id)">Add</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="editVaultDialogVal" width="650">
        <v-card class="form">
          <v-card-title class=" text-bold">Edit this vault</v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-text-field label="Vault Name" v-model="editVaultDialog.name"></v-text-field>
          <v-text-field label="Vault Description" v-model="editVaultDialog.description"></v-text-field>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="editVaultDialogVal = false">Cancel</v-btn>
            <v-btn type="submit" @click="editVault(this.editVaultDialog); editVaultDialogVal = false;">Edit
              Vault</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="deleteVaultDialogVal" width="650">
        <v-card class="form ">
          <v-card-title> Careful </v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-card-text>Are you really wanting to delete this vault?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="deleteVaultDialogVal = false">Cancel</v-btn>
            <v-btn @click="deleteVault(activeVault)">Delete</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="logoutDialog" width="650">
        <v-card class="form">
          <v-card-title>Logout</v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-card-text>Leaving so soon?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn @click="logoutDialog = false">Cancel</v-btn>
            <v-btn @click="logout">Logout</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Vault",
    props: ["id"],
    data() {
      return {
        addKeepDialogVal: false,
        editVaultDialogVal: false,
        editVaultDialog: {
          name: "",
          description: ""
        },
        deleteVaultDialogVal: false,
        logoutDialog: false
      };
    },
    computed: {
      activeVault() {
        return this.$store.state.userVaults.find(vault => vault.id == this.id);
      },
      userVaults() {
        return this.$store.state.userVaults;
      },
      vaultKeeps() {
        return this.$store.state.vaultKeeps;
      },
      userKeeps() {
        return this.$store.state.userKeeps;
      }
    },
    mounted() {
      this.$store.dispatch("getVaultKeeps", this.id)
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      },
      editVault(editedVault) { },
      deleteVault(vault) {
        this.$store.dispatch("deleteVault", vault);
        this.deleteVaultDialogVal = false;
      },
      removeKeep(keepId) {
        this.$store.dispatch("removeKeepFromVault", { keepId, vaultId: this.id });
      },
      addKeep(keepId) {
        this.$store.dispatch("addKeepToVault", { keepId, vaultId: this.id });
        this.addKeepDialogVal = false;
      }
    }
  };
</script>