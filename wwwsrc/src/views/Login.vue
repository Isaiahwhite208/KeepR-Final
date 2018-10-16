<template>
    <div class="TotalPage">
        <v-toolbar class="header" height="60rem" color="#494949" app>
            <v-spacer></v-spacer>
            <p class="header">KeepR'</p>
            <v-spacer></v-spacer>
        </v-toolbar>
        <div class="page-wrapper" style="padding-top: 3rem;">
            <section>
                <v-card class="login" color="#494949">
                    <v-container>
                        <v-container>
                            <v-card-title class=" form-title">
                                <p class="login-title">Login or Register to see more of KeepR</p>
                            </v-card-title>
                        </v-container>

                        <v-divider color="#66fcf1"></v-divider>

                        <form v-if="loginForm">
                            <p class="form-title">Login and get back at it!</p>
                            <v-text-field type="email" v-model="creds.email" label="Email Address"></v-text-field>
                            <v-text-field type="password" v-model="creds.password" label="Password"></v-text-field>
                            <v-btn color="#66fcf1" flat type="submit" @click="loginUser">Login</v-btn>
                        </form>

                        <form v-else>
                            <p class="form-title">Register to join the fun</p>
                            <v-text-field type="text" v-model="newUser.username" label="Name"></v-text-field>
                            <v-text-field type="email" v-model="newUser.email" label="Email Address"></v-text-field>
                            <v-text-field type="password" v-model="newUser.password" label="Password"></v-text-field>
                            <v-btn color="#66fcf1" flat type="submit" @click="register">Create Account</v-btn>
                        </form>
                        <div class="registry">
                            <div v-if="loginForm">
                                <p>No account, click to register</p>
                                <div>
                                    <v-btn color="#66fcf1" flat @click="loginForm = !loginForm">Register</v-btn>
                                </div>
                            </div>
                            <div v-else>
                                <p>Already have an account, click to login</p>
                                <div>
                                    <v-btn color="#66fcf1" flat @click="loginForm = !loginForm">Login</v-btn>
                                </div>
                            </div>
                        </div>
                    </v-container>
                </v-card>
            </section>
        </div>
        <div class="page-wrapper">
            <section>
                <v-card width="60rem" color="#494949">
                    <v-container>
                        <div>
                            <v-card-title class="footer">
                                <h2 class="see-more-title">KeepR minus the account</h2>
                            </v-card-title>
                            <v-divider color="#66fcf1"></v-divider>
                            <v-layout v-if="publicKeeps.length > 0">
                                <div class="keeps-container">
                                    <v-card v-for="keep in publicKeeps" :key="keep._id" class="keep-card" hover>
                                        <v-card-title primary-title>
                                            <div>
                                                <h5 class="keep-name">{{keep.name}}</h5>
                                                <p class="keep-description">{{keep.description}}</p>
                                            </div>
                                        </v-card-title>
                                    </v-card>
                                </div>
                            </v-layout>
                        </div>
                    </v-container>
                </v-card>
            </section>
            <v-dialog v-model="viewActiveKeepDialogVal" width="800">
                <v-card class="view-keep-card">
                    <v-card-title primary-title class="activeKeep-title">
                        <div>
                            <h5 class="activeKeep-name">{{activeKeep.name}}</h5>
                            <p class="activeKeep-description">{{activeKeep.description}}</p>
                        </div>
                        <div>
                            <p class="activeKeep-views">
                                Views: {{activeKeep.views}}
                            </p>
                        </div>
                    </v-card-title>
                </v-card>
            </v-dialog>
        </div>
    </div>
</template>

<script>
    export default {
        name: "login",
        data() {
            return {
                loginForm: true,
                creds: {
                    email: "",
                    password: ""
                },
                newUser: {
                    email: "",
                    password: "",
                    username: ""
                },
                activeKeep: {},
                viewActiveKeepDialogVal: false
            };
        },
        mounted() {
            this.$store.dispatch("authenticate");
            this.$store.dispatch("getPublicKeeps");
        },
        computed: {
            publicKeeps() {
                return this.$store.state.publicKeeps;
            }
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            },
            viewActiveKeep(keep) {
                this.activeKeep = keep;
                this.viewActiveKeepDialogVal = true;
                this.$store.dispatch("incrementViews", this.activeKeep);
            }
        }
    };

</script>