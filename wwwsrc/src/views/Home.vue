<template>
  <div class="heading-bar">
    <v-toolbar app class="heading-bar">
      <v-toolbar-title class="heading-title">THE KeepR'</v-toolbar-title>
      <v-card>
        <v-text-field class="heading-searchbar" label="Search KeepR'">
        </v-text-field>
      </v-card>
      <v-spacer></v-spacer>
      <v-btn flat color="#66fcf1" class=" logout" raised @click="logoutPrompt = true">logout</v-btn>
    </v-toolbar>
    <div class="page-wrapper">
      <section>
        <p class="heading">Welcome back {{user.username}}!</p>
      </section>
      <section>
        <v-card class="dashboard">
          <v-container row wrap>
            <v-flex xs12>
              <v-card-title class=" card-title">
                <p>Your Vaults</p>
                <v-spacer></v-spacer>
                <v-btn flat color="#66fcf1" @click="newVaultDialogVal = true" fab>
                  <v-btn flat color="#66fcf1">add</v-btn>
                </v-btn>
              </v-card-title>
              <div class="monitor">
                <p>Make a vault or two, because you dont have any.</p>
              </div>
            </v-flex>
          </v-container>
        </v-card>
        <v-divider color="#66fcf1"></v-divider>
        <v-card class="dashboard">
          <v-container row wrap>
            <v-flex xs12>
              <v-card-title class=" card-title">
                <p>Your Keeps</p>
                <v-spacer></v-spacer>
                <v-btn flat color="#66fcf1" @click="newKeepDialogVal = true" fab>
                  <v-btn flat color="#66fcf1">add</v-btn>
                </v-btn>
              </v-card-title>
              <div v-if="userKeeps.length > 0">
                <v-card v-for="keep in userKeeps" :key="keep.id">
                  <v-container>
                    <v-flex xs12>
                      <v-card-title></v-card-title>
                    </v-flex>
                  </v-container>
                </v-card>
              </div>
              <div v-else class="monitor">
                <p>You have no keeps at the moment. Make some?</p>
              </div>
            </v-flex>
          </v-container>
        </v-card>
      </section>
      <v-dialog v-model="newVaultDialogVal" width="800">
        <v-card class="prompt">
          <v-card-title>Create new vault</v-card-title>
          <v-text-field label="Title" v-model="newVaultDialog.name"></v-text-field>
          <v-text-field label="Description" v-model="newVaultDialog.description"></v-text-field>
          <v-card-actions>
            <v-btn flat color="#66fcf1" type="submit" @click="newVault; newVaultDialogVal = false;" class="">create
              vault</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="newKeepDialogVal" width="800">
        <v-card class="prompt">
          <v-card-title class="">Create new keep</v-card-title>
          <v-text-field label="Title" v-model="newKeepDialog.name"></v-text-field>
          <v-text-field label="Img Link" v-model="newKeepDialog.img"></v-text-field>
          <v-text-field label="Description" v-model="newKeepDialog.description"></v-text-field>
          <v-card-actions>
            <v-btn flat color="#66fcf1" type="submit" @click="newKeep(); newKeepDialogVal = false;" class="">create
              keep</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-dialog v-model="logoutPrompt" width="800">
        <v-card class="prompt">
          <v-card-title class="">Logout</v-card-title>
          <v-card-text class="">Are you sure you want to leave so soon?</v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn flat color="#66fcf1" @click="logoutPrompt = false">Cancel</v-btn>
            <v-btn flat color="#66fcf1" @click="logout">Logout</v-btn>
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
        newVaultDialog: {
          name: "",
          description: ""
        },
        newVaultDialogVal: false,
        newKeepDialog: {
          name: "",
          img: "",
          description: "",
          isPrivate: false
        },
        newKeepDialogVal: false,
        logoutPrompt: false
      };
    },
    computed: {
      user() {
        return this.$store.state.user;
      },
      userKeeps() {
        return this.$store.state.userKeeps;
      }
    },
    mounted() {
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    methods: {
      logout() {
        this.$store.dispatch("logout");
      },
      newVault() {
        this.$store.dispatch("newVault", this.newVaultDialog);
        this.newVaultDialog.name = "";
        this.newVaultDialog.description = "";
      },
      newKeep() {
        this.newKeepDialog.isPrivate = this.newKeepDialog.isPrivate ? 1 : 0;
        this.$store.dispatch("newKeep", this.newKeepDialog);
        this.newKeepDialog.name = "";
        this.newKeepDialog.img = "";
        this.newKeepDialog.description = "";
        this.newKeepDialog.isPrivate = false;
      }
    }
  };
</script>

<style scoped>
  .prompt {
    display: flex;
    flex-direction: column;
    color: #66fcf1;
    background-color: #494949;

  }

  .page-wrapper {
    height: 100%;
    width: calc(100% - 2rem);
    max-width: 100rem;
    margin: auto;
    margin-top: 2rem;
    padding-top: 64px;
    padding-bottom: 4rem;
    padding-left: 5rem;
    padding-right: 5rem;
    display: flex;
    flex-direction: column;
    background-color: #494949;
    overflow-y: auto;
    position: relative;
  }

  .fab {
    background-color: #66fcf1;
    border-radius: 2rem;
    border: solid 1px #66fcf1;
    height: 2.4rem;
    padding-left: 0.28rem;
    padding-right: 0.28rem;
    display: flex;
    align-items: center;
    margin-right: 1rem;
  }

  .dashboard {
    background-color: #494949;
  }

  .heading {
    margin-bottom: 2rem;
    font-size: 2rem;
    display: flex;
    flex-direction: center;
    justify-content: center;
    text-align: left;
    color: #66fcf1;
  }

  .heading-bar {
    background-color: #494949;
  }

  .heading-title {
    margin-top: 1rem;
    margin-bottom: 1rem;
    font-size: 3rem;
    display: flex;
    flex-direction: start;
    color: #66fcf1;
    background-color: #494949;
  }

  .heading-searchbar {
    height: 4rem;
    padding-left: 1rem;
    padding-right: 1rem;
    padding-top: 0.3rem;
    background-color: #494949;
  }

  .card-title {
    font-size: 2rem;
    display: flex;
    align-items: center;
    color: #66fcf1;
  }

  .monitor {
    margin: 5rem;
    color: orangered;
  }
</style>