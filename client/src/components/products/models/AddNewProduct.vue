<template>
  <v-dialog
    transition="dialog-top-transition"
    max-width="600"
    v-model="addNewDialog"
  >
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Opening from the top</v-toolbar>
        <v-card-text>
          <div class="text-h2 pa-12">Hello world!</div>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>
<script>
export default {
  name: 'AddNewProduct',
  props: {
    showAddNewModal: {
      type: Boolean,
      default: false,
      required: false
    }
  },
  data: () => ({
    addNewDialog: false
  }),
  computed: {},
  watch: {
    showAddNewModal: {
      immediate: true,
      handler(oldVal, newVal) {
        this.addNewDialog = newVal
      }
    }
  }
}
</script>
<style lang="scss">
@import '@/assets/styles/variables.scss';
</style>
