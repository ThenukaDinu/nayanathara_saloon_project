<template>
  <div>
    <div class="ml-5 appointment-by-type">
      <h1 class="mb-10">Appointments by type</h1>
      <v-row class="ml-1">
        <v-menu
          ref="menu"
          v-model="menu"
          :close-on-content-click="false"
          :return-value.sync="dates"
          transition="scale-transition"
          offset-y
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-combobox
              v-model="dates"
              multiple
              chips
              small-chips
              label="Select Date Range"
              prepend-icon="mdi-calendar"
              readonly
              v-bind="attrs"
              v-on="on"
            ></v-combobox>
          </template>
          <v-date-picker v-model="dates" range no-title scrollable>
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="menu = false"> Cancel </v-btn>
            <v-btn
              text
              color="primary"
              @click="changeDateRange(dates, 'by-type')"
            >
              OK
            </v-btn>
          </v-date-picker>
        </v-menu>
      </v-row>
      <v-data-table
        :headers="headers"
        :items="appointmentsByTypeData"
        :items-per-page="10"
        class="elevation-1 mt-6"
      ></v-data-table>
    </div>
  </div>
</template>
<script>
import moment from 'moment'
import reports from '@/assets/js/api/reports'
export default {
  name: 'AppointmentReports',
  mixins: [reports],
  data: () => ({
    dates: [],
    menu: false,
    headers: [
      {
        text: 'Appointment Type',
        align: 'start',
        sortable: true,
        value: 'typeText'
      },
      { text: 'Count', value: 'count', sortable: true }
    ],
    appointmentsByTypeData: []
  }),
  methods: {
    changeDateRange(dates, type) {
      if (type === 'by-type') {
        this.$refs.menu.save(dates)
        this.getAppointsByType()
      }
    },
    async getAppointsByType() {
      this.appointsByType(
        {
          url: '/Report',
          method: 'POST',
          data: {
            StartDate: this.dates[0],
            EndDate: this.dates[1]
          }
        },
        response => {
          this.appointmentsByTypeData = response.data
        },
        error => {
          console.error(error)
        }
      )
    }
  },
  created() {
    this.dates.push(
      moment().add(-1, 'M').format('YYYY-MM-DD'),
      moment().format('YYYY-MM-DD')
    )
    this.getAppointsByType()
  }
}
</script>
<style lang="scss"></style>
