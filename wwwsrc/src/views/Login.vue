<template>
    <div class="full-layout">
        <v-container class="full-layout">
            <v-toolbar app class="full-toolbar">
                <v-toolbar-title class="toolbar-title">THE KeepR'</v-toolbar-title>
                <v-card class="searchbar">
                    <v-text-field label="Search KeepR'">
                    </v-text-field>
                </v-card>
            </v-toolbar>
            <div class="page-wrapper">
                <v-card class="login">
                    <v-container>
                        <v-card-title>
                            <p class="login-title">Login or Register to see more of KeepR'</p>
                        </v-card-title>
                        <form v-if="loginForm" @submit.prevent="loginUser">
                            <p>Create or Login to gain more fun stuff!</p>
                            <v-text-field class="text-login" type="email" v-model="creds.email" label="Email Address"></v-text-field>
                            <v-text-field class="text-login" type="password" v-model="creds.password" label="Password"></v-text-field>
                            <v-btn flat color="#66fcf1" type="submit" @click="loginUser">Login</v-btn>
                        </form>
                        <form v-else @submit.prevent="register">
                            <p>Create or Login to gain more fun stuff!</p>
                            <v-text-field type="text" v-model="newUser.username" label="Name"></v-text-field>
                            <v-text-field type="email" v-model="newUser.email" label="Email Address"></v-text-field>
                            <v-text-field type="password" v-model="newUser.password" label="Password"></v-text-field>
                            <v-btn flat color="#66fcf1" type="submit" @click="register">Create
                                Account</v-btn>
                        </form>
                        <div>
                            <div v-if="loginForm">
                                <p>Brand new? Register with us.</p>
                                <div>
                                    <v-btn flat color="#66fcf1" @click="loginForm = !loginForm">Register</v-btn>
                                </div>
                            </div>
                            <div v-else>
                                <p>Previous member? Come login.</p>
                                <div>
                                    <v-btn flat color="#66fcf1" @click="loginForm = !loginForm">Login</v-btn>
                                </div>
                            </div>
                        </div>
                    </v-container>
                </v-card>
            </div>
        </v-container>
    </div>
</template>

<script>
    export default {
        name: "login",
        mounted() {
            this.$store.dispatch("authenticate");
        },
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
                }
            };
        },
        methods: {
            register() {
                this.$store.dispatch("register", this.newUser);
            },
            loginUser() {
                this.$store.dispatch("login", this.creds);
            }
        }
    };
</script>

<style scoped>
    .page-wrapper {
        display: flex;
        flex-direction: row;
        justify-content: center;
        background-color: #2b2b2b;
    }

    .full-layout {
        background-color: #494949;
    }

    .searchbar {
        height: 4rem;
        padding-left: 1rem;
        padding-right: 1rem;
        padding-top: 0.3rem;
        background-color: #494949;
    }

    .full-toolbar {
        background-color: #494949;
    }

    .toolbar-title {
        margin-top: 1rem;
        margin-bottom: 1rem;
        font-size: 3rem;
        display: flex;
        flex-direction: center;
        justify-content: center;
        color: #66fcf1;
        background-color: #494949;
    }

    .login {
        width: 35rem;
        margin-top: 5rem;
        margin-bottom: 5rem;
        background-color: #494949;
        color: white;
    }

    .login-title {
        display: flex;
        flex-direction: row;
        font-size: 2rem;
        color: #66fcf1;
    }
</style>