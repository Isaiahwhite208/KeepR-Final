import Vue from 'vue'
import Vuex from 'vuex'
import Axios from 'axios'
import router from './router'

Vue.use(Vuex)

let auth = Axios.create({
  baseURL: "//localhost:5000/account/",
  timeout: 3000,
  withCredentials: true
})

let api = Axios.create({
  baseURL: "//localhost:5000/api/",
  timeout: 3000,
  withCredentials: true
})

export default new Vuex.Store({
  state: {
    user: {},
    userKeeps: [],
    userVaults: [],
    activeKeep: {},
    vaultKeeps: [],
    publicKeeps: {},

  },
  mutations: {
    setUser(state, user) {
      state.user = user
    },
    setUserKeeps(state, userKeeps) {
      state.userKeeps = userKeeps;
    },
    setUserVaults(state, userVaults) {
      state.userVaults = userVaults;
    },
    setActiveKeep(state, activeKeep) {
      state.activeKeep = activeKeep;
    },
    setPublicKeeps(state, publicKeeps) {
      state.publicKeeps = publicKeeps;
    },
    setVaultKeeps(state, keeps) {
      state.vaultKeeps = keeps;
    }
  },
  actions: {
    register({ commit, dispatch }, newUser) {
      auth.post('register', newUser)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('[registration failed] :', e)
        })
    },
    authenticate({ commit, dispatch }) {
      auth.get('authenticate')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('not authenticated')
        })
    },
    login({ commit, dispatch }, creds) {
      auth.post('login', creds)
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'home' })
        })
        .catch(e => {
          console.log('Login Failed')
        })
    },
    logout({ commit, dispatch }, creds) {
      auth.delete('logout')
        .then(res => {
          commit('setUser', res.data)
          router.push({ name: 'login' })
        })
        .catch(e => {
          console.log('Failed to logout')
        })
    },
    getPublicKeeps({ commit }) {
      api.get('keeps')
        .then(res => {
          commit('setPublicKeeps', res.data)
        })
        .catch(e => {
          console.log('Failed to get public keep(s)')
        })
    },
    getUserKeeps({ commit }) {
      api.get('keeps/myKeeps')
        .then(res => {
          commit('setUserKeeps', res.data)
        })
        .catch(e => {
          console.log('Failed to get users keep(s)')
        })
    },
    newKeep({ commit, dispatch }, creds) {
      api.post('keeps', creds)
        .then(res => {
          dispatch('getUserKeeps')
        })
        .catch(e => {
          console.log('Failed to create keep')
        })
    },
    deleteKeep({ commit, dispatch }, keep) {
      api.delete('keeps/' + keep.id)
        .then(res => {
          dispatch('getUserKeeps')
        })
        .catch(e => {
          console.log('Failed to delete keep')
        })
    },
    newVault({ commit, dispatch }, creds) {
      api.post('vaults', creds)
        .then(res => {
          dispatch('getUserVaults')
        })
        .catch(e => {
          console.log('Failed to create new vault')
        })
    },
    getUserVaults({ commit }) {
      api.get('vaults/myVaults')
        .then(res => {
          commit('setUserVaults', res.data)
        })
        .catch(e => {
          console.log('Failed to get user vaults')
        })
    },
    deleteVault({ commit, dispatch }, vault) {
      api.delete('vaults/' + vault.id)
        .then(res => {
          dispatch('getUserVaults')
          router.push({ name: "home" });
        })
        .catch(e => {
          console.log('Failed to delete vault')
        })
    },
    incrementViews({ commit, dispatch }, activeKeep) {
      activeKeep.views++
      api.put('keeps', activeKeep)
        .catch(e => {
          console.log('Failed to increment active keep views')
        })
    },
    addKeepToVault({ commit, dispatch }, payload) {
      api.post('vaults/keeps', payload)
        .then(res => {
          dispatch("getVaultKeeps", payload.vaultId)
        })
    },
    getVaultKeeps({ commit, dispatch }, vaultId) {
      api.get('vaults/keeps/' + vaultId)
        .then(res => {
          commit('setVaultKeeps', res.data)
        })
    },
    removeKeepFromVault({ commit, dispatch }, data) {
      api.put('vaults/keeps/', data)
        .then(res => {
          dispatch("getVaultKeeps", data.vaultId)
        })
    }
  }
})