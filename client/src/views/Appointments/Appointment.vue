<template>
  <div class="appointments">
    <v-card>
      <v-tabs color="#897a58" right>
        <v-tab class="v-tab" v-if="isUserAdmin">Manage Appointments</v-tab>
        <v-tab class="v-tab">My appointments</v-tab>
        <v-tab class="v-tab">Place Appointment</v-tab>

        <v-tab-item v-for="n in 3" :key="n">
          <v-container fluid class="mt-3">
            <ManageAppointments v-if="n === 1" />
            <MyAppointments v-if="n === 2" />
            <PlaceAppointments v-if="n === 3" />
          </v-container>
        </v-tab-item>
      </v-tabs>
    </v-card>
  </div>
</template>
<script>
import ManageAppointments from '../../components/appointments/ManageAppointments.vue'
import MyAppointments from '../../components/appointments/MyAppointments.vue'
import PlaceAppointments from '../../components/appointments/PlaceAppointments.vue'
export default {
  data() {
    return {
      items: [
        { title: 'Dashboard', icon: 'mdi-view-dashboard' },
        { title: 'Account', icon: 'mdi-account-box' },
        { title: 'Admin', icon: 'mdi-gavel' }
      ],
      drawer: true
    }
  },
  computed: {
    user() {
      return this.$store.state.user.user
    },
    isUserAdmin() {
      return this.user && this.user.userRoles
        ? this.user.userRoles.some(role => role === 'Admin')
        : false
    }
  },
  components: { ManageAppointments, MyAppointments, PlaceAppointments }
}
</script>
<style lang="scss">
.appointments {
  margin: 0;
  padding: 0;
}
</style>
