<template>
  <div>
    <v-toolbar height="60rem" app>
      <v-btn flat class=" logout" raised @click="logoutDialog = true">Logout</v-btn>
    </v-toolbar>
    <div class="page-wrapper">
      <section>
        <h1 class=" heading">welcome to <br /> {{user.username}}'s dashboard</h1>
      </section>
      <v-divider style="margin-bottom: 2rem;" color="#66fcf1"></v-divider>
      <section>
        <v-card class="dashboard-vaults">
          <v-container row wrap>
            <v-flex xs12>
              <v-card-title class=" card-heading">
                <p>Your Vaults</p>
                <v-btn flat color="#66fcf1" @click="newVaultDialogVal = true" fab>Add</v-btn>
              </v-card-title>
              <v-divider color="#66fcf1"></v-divider>
              <v-layout v-if="userVaults.length > 0">
                <div class="vaults-container">
                  <v-card v-for="vault in userVaults" :key="vault._id" class="vault-card" hover>
                    <v-card-title primary-title>
                      <div>
                        <h4 class="vault-name">{{vault.name}}</h4>
                        <p class="vault-description">{{vault.description}}</p>
                      </div>
                    </v-card-title>
                    <div class="v-card-btn">
                      <v-btn flat @click="$router.push({ name: 'vault', params: { id: vault.id }})" class="vault-card-actions"
                        fab>Open in new</v-btn>
                    </div>
                  </v-card>
                </div>
              </v-layout>
            </v-flex>
          </v-container>
        </v-card>
        <v-divider style="margin-top: 3rem; margin-bottom: 3rem;" color="#66fcf1"></v-divider>
        <v-card class="dashboard-keeps">
          <v-container row wrap>
            <v-flex xs12>
              <v-card-title class=" card-heading">
                <p>Your keeps</p>
                <v-btn flat color="#66fcf1" @click="newKeepDialogVal = true" fab>Add</v-btn>
              </v-card-title>
              <v-divider color="#66fcf1"></v-divider>
              <v-layout v-if="userKeeps.length > 0">
                <div class="keeps-container">
                  <v-card v-for="keep in userKeeps" :key="keep._id" class="keep-card" hover>
                    <div>
                      <v-btn flat @click="viewKeep(keep)" class="keepActions" fab>Views</v-btn>
                      <v-btn flat class="keepActions" fab>Save</v-btn>
                      <v-btn flat class="keepActions" fab>Share</v-btn>
                    </div>
                    <v-card-title primary-title>
                      <div>
                        <h4 class="keep-name">{{keep.name}}</h4>
                        <p class="keep-description">{{keep.description}}</p>
                      </div>
                    </v-card-title>
                  </v-card>
                </div>
              </v-layout>
              <div v-else class="no-keeps">
                <p class="">You currently don't have any keeps</p>
              </div>
            </v-flex>
          </v-container>
        </v-card>
        <v-divider style="margin-top: 3rem;" color="#66fcf1"></v-divider>
      </section>
      <v-dialog v-model="newVaultDialogVal" width="800">
        <v-card class="form">
          <v-card-title class=" text-bold">Create new vault</v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-text-field label="Title" v-model="newVaultDialog.name"></v-text-field>
          <v-text-field label="Description" v-model="newVaultDialog.description"></v-text-field>
          <v-card-actions>
            <v-btn flat type="submit" @click="newVault(); newVaultDialogVal = false;" class="">Create Vault</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="newKeepDialogVal" width="800">
        <v-card class="form">
          <v-card-title class="">Create new keep</v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-text-field label="Title" v-model="newKeepDialog.name"></v-text-field>
          <v-text-field label="Img Link" v-model="newKeepDialog.img"></v-text-field>
          <v-text-field label="Description" v-model="newKeepDialog.description"></v-text-field>
          <input type="checkbox" label="Make Keep Private" v-model="newKeepDialog.isPrivate"> Make Private
          <v-card-actions>
            <v-btn flat type="submit" @click="newKeep(); newKeepDialogVal = false;" class="">Create Keep</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="editKeepDialogVal" width="800">
        <v-card class="form">
          <v-card-title class="">Edit this keep</v-card-title>
          <v-divider color="#66fcf1"></v-divider>
          <v-text-field label="Title" v-model="editKeepDialog.name"></v-text-field>
          <v-text-field label="Img Link" v-model="editKeepDialog.img"></v-text-field>
          <v-text-field label="Description" v-model="editKeepDialog.description"></v-text-field>
          <input type="checkbox" label="Make Keep Private" v-model="editKeepDialog.isPrivate"> Make Private
          <v-card-actions>
            <v-btn flat type="submit" @click="editKeep(keep); editKeepDialogVal = false;" class="">Edit Keep</v-btn>
            <v-btn flat @click="editKeepDialogVal = false" class="">Cancel</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </div>
  </div>
</template>

<script>
  export default {
    name: "home",
    data() {
      return {
        activeVault: {},
        newVaultDialogVal: false,
        newVaultDialog: {
          name: "",
          description: ""
        },

        activeKeep: {},
        newKeepDialogVal: false,
        editKeepDialogVal: false,
        editVaultDialogVal: false,
        viewKeepDialogVal: false,
        deleteKeepDialogVal: false,
        newKeepDialog: {
          name: "",
          img: "",
          description: "",
          isPrivate: false
        },
        editKeepDialog: {
          name: "",
          img: "",
          description: "",
          isPrivate: false
        },

        logoutDialog: false
      };
    },

    computed: {
      user() {
        return this.$store.state.user;
      },
      userKeeps() {
        return this.$store.state.userKeeps;
      },
      userVaults() {
        return this.$store.state.userVaults;
      }
    },
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      } else {
        this.$store.dispatch("getUserKeeps");
        this.$store.dispatch("getUserVaults");
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      },
      newVault() {
        let newVaultData = {
          name: this.newVaultDialog.name,
          description: this.newVaultDialog.description
        };
        this.$store.dispatch("newVault", newVaultData);
        this.newVaultDialog.name = "";
        this.newVaultDialog.description = "";
      },
      viewVault(vault) {
        this.activeVault = vault;
        this.$store.dispatch("setActiveVault", activeVault);
        this.$router.push({ name: "" });
      },
      newKeep() {
        let newKeepData = {
          name: this.newKeepDialog.name,
          img: this.newKeepDialog.img,
          description: this.newKeepDialog.description
        };
        newKeepData.isPrivate = this.newKeepDialog.isPrivate ? 1 : 0;
        this.$store.dispatch("newKeep", newKeepData);
        this.newKeepDialog.name = "";
        this.newKeepDialog.img = "";
        this.newKeepDialog.description = "";
        this.newKeepDialog.isPrivate = false;
      },
      editKeep(keep) {
        this.editKeepDialogVal = false;
        this.$store.dispatch("editKeep", keep);
      },
      viewKeep(keep) {
        this.activeKeep = keep;
        this.viewKeepDialogVal = true;
        this.$store.dispatch("incrementViews", this.activeKeep);
      },
      deleteKeep(keep) {
        this.$store.dispatch("deleteKeep", keep);
        this.deleteKeepDialogVal = false;
        this.viewKeepDialogVal = false;
      }
    }
  };
</script>