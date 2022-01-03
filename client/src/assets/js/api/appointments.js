import baseApiExecuter from '@/assets/js/api/baseExecuter'
export default {
  mixins: [baseApiExecuter],
  methods: {
    async appointmentsGet(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    },
    async appointmentUpdateStatus(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    },
    async postAppointment(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method,
          headers: data.headers
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    }
  }
}
