<template>
  <v-dialog transition="dialog-top-transition" max-width="800" v-model="dialog">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Invoice</v-toolbar>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>
<script>
export default {
  data: () => ({
    dialog: false
  }),
  props: {
    appointment: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  methods: {
    openModal() {
      this.dialog = true
    },
    closeModal() {
      this.dialog = false
    }
  }
}
</script>
<style lang="scss"></style>
