<template>
  <div class="manage-orders mx-6">
    <h2 class="mb-5">Manage Orders</h2>
    <v-data-table
      :headers="ordersHeaders"
      :items="ordersData"
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
                  <v-radio label="Placed" color="orange" :value="0"></v-radio>
                  <v-radio
                    label="Paid"
                    color="orange darken-3"
                    :value="1"
                  ></v-radio>
                  <v-radio
                    :value="2"
                    label="Approved"
                    color="red darken-3"
                  ></v-radio>
                  <v-radio label="Delivered" color="green" :value="3"></v-radio>
                  <v-radio
                    :value="4"
                    label="Completed"
                    color="#224638"
                  ></v-radio>
                  <v-radio
                    label="Returned"
                    color="pink darken-3"
                    :value="5"
                  ></v-radio>
                  <v-radio
                    label="Canceled"
                    color="gray darken-3"
                    :value="6"
                  ></v-radio>
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
        <v-btn
          small
          @click="changeStatus(item)"
          color="green"
          class="white--text"
          >Change Status</v-btn
        >
      </template>
    </v-data-table>
  </div>
</template>

<script>
import order from '@/assets/js/api/order'
import moment from 'moment'
import { orderStatus } from '@/assets/js/enums/orderEnum'
import objectHelper from '@/assets/js/healpers/objectHelper'
export default {
  name: 'ManageOrders',
  mixins: [order, objectHelper],
  data: () => ({
    ordersData: [],
    ordersLoading: false,
    ordersHeaders: [
      {
        text: 'Created Date',
        align: 'start',
        sortable: true,
        value: 'createdDateFormatted'
      },
      { text: 'Mobile No', value: 'mobileNo', sortable: true },
      { text: 'Order Status', value: 'orderStatusText', sortable: true },
      { text: 'Total Amount', value: 'totalAmount', sortable: true },
      { text: 'Actions', value: 'actions', sortable: false }
    ],
    statusSelected: 0,
    dialogStatus: false
  }),
  methods: {
    getAllOrders() {
      this.getOrders(
        {
          url: '/Orders',
          method: 'GET'
        },
        response => {
          this.ordersData = response.data.map(d => {
            return {
              ...d,
              createdDateFormatted: moment(
                new Date(Date.parse(d.createdDate))
              ).format('MMM DD YYYY'),
              orderStatusText: this.getKeyByValue(orderStatus, d.orderStatus)
            }
          })
        },
        error => {
          this.ordersLoading = false
          console.error(error)
        }
      )
    },
    dialogStatusConfirm() {},
    changeStatus(item) {
      this.selectedIndex = this.ordersData.indexOf(item)
      this.selectedItem = item
      this.statusSelected = this.selectedItem.orderStatus
      this.dialogStatus = true
    },
    closeDialogStatus() {
      this.dialogStatus = false
      this.$nextTick(() => {
        this.selectedItem = {}
        this.selectedIndex = -1
      })
    }
  },
  computed: {},
  created() {
    this.getAllOrders()
  }
}
</script>

<style lang="scss" scoped>
.manage-orders {
  margin-top: 30px;
}
</style>
