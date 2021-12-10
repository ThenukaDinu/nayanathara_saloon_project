import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'
import store from './store'
import Toast from './plugins/vue-toastification'

Vue.config.productionTip = false

new Vue({
  vuetify,
  router,
  store,
  Toast,
  render: h => h(App)
}).$mount('#app')
