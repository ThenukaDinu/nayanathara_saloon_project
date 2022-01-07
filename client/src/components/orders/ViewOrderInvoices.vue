<template>
  <v-dialog width="1000" v-model="dialog" class="view-order-invoices">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Product Invoices</v-toolbar>
        <v-card-text>
          <div class="mx-6 my-6">
            <v-card
              class="mt-10"
              v-for="(inv, index) in invoicesData"
              :key="index"
            >
              <v-row class="mx-4">
                <v-col> <h3>Invoice No</h3> </v-col>
                <v-col> {{ inv.invoiceNo }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Created Date</h3> </v-col>
                <v-col>
                  {{ inv.createdDateFormatted }}
                </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Amount</h3> </v-col>
                <v-col> LKR: {{ inv.amount }} </v-col>
              </v-row>
            </v-card>
          </div>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
import order from '@/assets/js/api/order'
import moment from 'moment'
import appointments from '../../assets/js/api/appointments'
export default {
  name: 'viewChangeStatus',
  mixins: [order, appointments],
  props: {
    type: {
      type: String,
      required: false,
      default: 'order'
    },
    order: {
      type: Object,
      required: false,
      default: () => undefined
    },
    appointment: {
      type: Object,
      required: false,
      default: () => undefined
    }
  },
  data: () => ({
    dialog: false,
    invoicesData: []
  }),
  methods: {
    openModal() {
      if (this.type === 'order') {
        this.getInvoices()
      } else {
        this.getAppoinmentInvoices()
      }
      this.dialog = true
    },
    getInvoices() {
      const orderSelected = this.order
      const self = this
      this.GetInvoicesForOrder(
        {
          url: `/Invoice/appointment/${orderSelected.id}`,
          method: 'GET'
        },
        response => {
          const temp = response.data

          temp.createdDateFormatted = moment(
            new Date(Date.parse(response.data.createdDate))
          ).format('MMM DD YYYY')
          self.invoicesData.push(temp)
        },
        error => {
          console.error(error)
        }
      )
    },
    getAppoinmentInvoices() {
      const orderSelected = this.appointment
      const self = this
      this.GetInvoicesForAppointment(
        {
          url: `/Invoice/getAllInvoiceForProductOrder/${orderSelected.id}`,
          method: 'GET'
        },
        response => {
          self.invoicesData = response.data.map(a => {
            return {
              ...a,
              createdDateFormatted: moment(
                new Date(Date.parse(a.createdDate))
              ).format('MMM DD YYYY')
            }
          })
        },
        error => {
          console.error(error)
        }
      )
    },
    closeModal() {
      this.dialog = false
      this.$nextTick(() => {
        this.order = null
      })
    }
  }
}
</script>

<style lang="scss" scoped></style>
