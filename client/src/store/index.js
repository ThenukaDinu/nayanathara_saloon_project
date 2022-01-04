import Vue from 'vue'
import user from './modules/user'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'
import * as Cookies from 'js-cookie'
import cart from './modules/cart'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isMenuHide: true
  },
  mutations: {
    setIsMenuHide(state, payload) {
      state.isMenuHide = payload
    }
  },
  actions: {},
  modules: {
    user: user,
    cart: cart
  },
  plugins: [
    createPersistedState({
      paths: ['user', 'cart'],
      storage: {
        getItem: key => Cookies.get(key),
        setItem: (key, value) => {
          Cookies.set(key, value, { expires: 3, secure: true })
        },
        removeItem: key => Cookies.remove(key)
      }
    })
  ]
})
