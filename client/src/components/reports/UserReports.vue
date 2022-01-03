<template>
  <div>
    <div class="mx-4 mt-10 appointment-amounts">
      <h1 class="mb-10">User Appointment Counts</h1>
      <v-row class="ml-1">
        <v-select
          v-model="selectedYear"
          :items="years"
          label="Select a year"
          outlined
          @change="getCustomerAppointmentCounts"
        ></v-select>
      </v-row>
      <v-data-table
        :headers="customerAppointmentCountHeaders"
        :items="customerAppointmentCountData"
        :items-per-page="10"
        class="elevation-1 mt-6"
        :loading="customerAppointmentCountLoading"
      ></v-data-table>
    </div>
    <div class="mx-4 mt-10 appointment-amounts">
      <h1 class="mb-10">How Many New Users Per Month</h1>
      <v-row class="ml-1">
        <v-select
          v-model="selectedYear2"
          :items="years"
          label="Select a year"
          outlined
          @change="getHowManyUsersPerMonth"
        ></v-select>
      </v-row>
      <v-data-table
        :headers="howManyUsersPerMonthtHeaders"
        :items="howManyUsersPerMonthData"
        :items-per-page="10"
        class="elevation-1 mt-6"
        :loading="howManyUsersPerMonthtLoading"
      ></v-data-table>
    </div>
  </div>
</template>
<script>
import reports from '@/assets/js/api/reports'
export default {
  name: 'UserReports',
  mixins: [reports],
  data: () => ({
    customerAppointmentCountData: [],
    howManyUsersPerMonthData: [],
    selectedYear: new Date().getFullYear(),
    selectedYear2: new Date().getFullYear(),
    years: [],
    customerAppointmentCountLoading: false,
    howManyUsersPerMonthtLoading: false,
    customerAppointmentCountHeaders: [
      {
        text: 'Email',
        align: 'start',
        sortable: true,
        value: 'email'
      },
      {
        text: 'Joined Date',
        sortable: true,
        value: 'joinDateFormatted'
      },
      { text: 'Appointment Count', value: 'appointmentCount', sortable: true }
    ],
    howManyUsersPerMonthtHeaders: [
      {
        text: 'Month',
        align: 'start',
        sortable: true,
        value: 'monthText'
      },
      { text: 'Users Count', value: 'usersCount', sortable: true }
    ]
  }),
  methods: {
    generateArrayOfYears(howMany) {
      var max = new Date().getFullYear()
      var min = max - howMany
      var years = []

      for (var i = max; i >= min; i--) {
        years.push(i)
      }
      return years
    },
    getCustomerAppointmentCounts() {
      this.customerAppointmentCountLoading = true
      this.customerAppointmentCounts(
        {
          url: '/Report/customersAppointmentCounts',
          method: 'POST',
          data: {
            Year: this.selectedYear
          }
        },
        response => {
          this.customerAppointmentCountData = response.data
          this.customerAppointmentCountLoading = false
        },
        error => {
          this.customerAppointmentCountLoading = false
          console.error(error)
        }
      )
    },
    getHowManyUsersPerMonth() {
      this.howManyUsersPerMonthtLoading = true
      this.howManyUsersPerMonth(
        {
          url: '/Report/howManyUsersPerMonth',
          method: 'POST',
          data: {
            Year: this.selectedYear2
          }
        },
        response => {
          this.howManyUsersPerMonthData = response.data
          this.howManyUsersPerMonthtLoading = false
        },
        error => {
          this.howManyUsersPerMonthtLoading = false
          console.error(error)
        }
      )
    }
  },
  created() {
    this.years = this.generateArrayOfYears(20)
    this.getCustomerAppointmentCounts()
    this.getHowManyUsersPerMonth()
  }
}
</script>
<style lang="scss"></style>
