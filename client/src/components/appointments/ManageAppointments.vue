<template>
  <div v-if="loading" class="loadingProgress">
    <v-progress-circular
      indeterminate
      size="60"
      color="#224638"
    ></v-progress-circular>
  </div>
  <v-data-table
    v-else
    :headers="headers"
    :items="appointments"
    sort-by="calories"
    class="elevation-1"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>

        <v-dialog v-model="dialogStatus" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Change status</v-card-title>
            <v-card-text>
              <v-radio-group v-model="statusSelected" column>
                <v-radio label="Created" color="orange" :value="0"></v-radio>
                <v-radio
                  label="Approved"
                  color="orange darken-3"
                  :value="1"
                ></v-radio>
                <v-radio
                  :value="2"
                  label="Rejected"
                  color="red darken-3"
                ></v-radio>
                <v-radio
                  label="InProgress"
                  color="indigo darken-3"
                  :value="3"
                ></v-radio>
                <v-radio :value="4" label="Completed" color="#224638"></v-radio>
                <v-radio
                  label="OnHold"
                  color="pink darken-3"
                  :value="5"
                ></v-radio>
                <v-radio
                  label="Canceled"
                  color="gray darken-3"
                  :value="6"
                ></v-radio>
                <v-radio label="Refunded" color="#c4af7e" :value="7"></v-radio>
                <v-radio label="Paid" color="green" :value="8"></v-radio>
              </v-radio-group>
            </v-card-text>
            <v-divider class="mx-4"></v-divider>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="gray" @click="closeDialogStatus">Cancel</v-btn>
              <v-btn
                color="#56896c"
                class="ml-5 white--text"
                @click="dialogStatusConfirm"
                >OK</v-btn
              >
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-btn small @click="changeStatus(item)" color="green" class="white--text"
        >Change Status</v-btn
      >
    </template>
    <template v-slot:no-data>
      <v-btn color="primary" @click="initialize"> Reset </v-btn>
    </template>
  </v-data-table>
</template>
<script>
import appointments from '@/assets/js/api/appointments'
import {
  appointmentType,
  appointmentStatus
} from '@/assets/js/enums/appointmentEnum'
import moment from 'moment'
import objectHelper from '../../assets/js/healpers/objectHelper'
export default {
  name: 'ManageAppointments',
  mixins: [appointments, objectHelper],
  data: () => ({
    loading: false,
    statusSelected: 0,
    dialog: false,
    dialogStatus: false,
    headers: [
      {
        text: 'Appointment Date',
        align: 'start',
        sortable: false,
        value: 'appoinmentDate'
      },
      { text: 'Created Date', value: 'createdDate' },
      { text: 'Completed Date', value: 'completedDate' },
      { text: 'Duration (mins)', value: 'durationInMins' },
      { text: 'Status', value: 'statusText' },
      { text: 'Type', value: 'typeText' },
      { text: "User's Email", value: 'userEmail' },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    appointments: [],
    responseData: [],
    selectedIndex: -1,
    selectedItem: {}
  }),
  computed: {},
  async created() {
    await this.getAppointments()
  },
  mounted() {
    //
  },
  methods: {
    initialize() {
      this.appointments = this.responseData.map(a => {
        return {
          ...a,
          typeText: this.getKeyByValue(appointmentType, a.type),
          statusText: this.getKeyByValue(appointmentStatus, a.status),
          userEmail: a.user.email,
          appoinmentDate: moment(new Date(Date.parse(a.appoinmentDate))).format(
            'MMM DD YYYY'
          ),
          completedDate: a.completedDate
            ? moment(new Date(Date.parse(a.completedDate))).format(
                'MMM DD YYYY'
              )
            : '-',
          createdDate: moment(new Date(Date.parse(a.createdDate))).format(
            'MMM DD YYYY'
          )
        }
      })
      this.loading = false
    },
    changeStatus(item) {
      this.selectedIndex = this.appointments.indexOf(item)
      this.selectedItem = item
      this.statusSelected = this.selectedItem.status
      this.dialogStatus = true
    },

    dialogStatusConfirm() {
      const appointmentObj = this.appointments.find(
        a => a.id === this.selectedItem.id
      )
      this.appointmentUpdateStatus(
        {
          url: `/Appointment/${this.selectedItem.id}/status-update`,
          method: 'PUT',
          data: {
            Status: this.statusSelected
          }
        },
        () => {
          this.$toast.success(
            `Appoinment status changed to ${this.getKeyByValue(
              appointmentStatus,
              this.statusSelected
            )} successfully`
          )
          appointmentObj.status = this.statusSelected
          appointmentObj.statusText = this.getKeyByValue(
            appointmentStatus,
            this.statusSelected
          )
        },
        error => {
          console.error(error)
          this.$toast.error('Status update failed please contact support')
        }
      )
      this.closeDialogStatus()
    },

    close() {
      this.dialogStatus = false
      this.$nextTick(() => {
        this.selectedItem = {}
        this.selectedIndex = -1
      })
    },

    closeDialogStatus() {
      this.dialogStatus = false
      this.$nextTick(() => {
        this.selectedItem = {}
        this.selectedIndex = -1
      })
    },

    async getAppointments() {
      this.loading = true
      await this.appointmentsGet(
        {
          url: '/Appointment',
          Method: 'GET'
        },
        response => {
          this.responseData = response.data
          this.initialize()
        },
        error => {
          this.loading = false
          console.error(error)
        }
      )
    }
  }
}
</script>
<style lang="scss"></style>
