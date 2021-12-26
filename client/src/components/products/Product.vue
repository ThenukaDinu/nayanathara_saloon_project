<template>
  <v-card
    v-if="product"
    :loading="loading"
    :class="['product my-7 mx-3']"
    max-width="287"
    v-show="product.isShow"
  >
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>
    <span
      @click="isUserAdmin && editProduct()"
      :class="[isUserAdmin ? 'pointer' : '']"
    >
      <v-img
        height="250"
        :src="'https://cdn.vuetifyjs.com/images/cards/cooking.png'"
      ></v-img>
      <v-card-title>{{ product.name }}</v-card-title>
      <v-card-text>
        <Like :product="product" />
        <div class="my-4 overline">
          <b>LKR {{ product.price }}</b>
        </div>
        <div>
          {{ product.description }}
        </div>
      </v-card-text>
      <v-divider class="mx-4"></v-divider>
    </span>
    <DeleteConfirmation
      ref="deleteConfirmationRef"
      :product="product"
      @delete-product="deleteProductConfirm"
      @delete-reject="deleteReject"
    />
    <EditProduct
      ref="editProductRef"
      v-if="product"
      :product="product"
      @update-product="updateProduct"
    />
    <v-card-actions>
      <v-btn v-if="isUserAdmin" color="error" outlined @click="deleteProduct">
        Delete
      </v-btn>
      <v-btn color="primary" outlined @click="buyProduct"> View </v-btn>
      <v-btn color="success" outlined @click="buyProduct"> Buy </v-btn>
    </v-card-actions>
  </v-card>
</template>
<script>
import Like from './Like.vue'
import DeleteConfirmation from './models/DeleteConfirmation.vue'
import product from '@/assets/js/api/product'
import EditProduct from './models/EditProduct.vue'
export default {
  name: 'product',
  mixins: [product],
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    loading: false,
    selection: 1
  }),
  methods: {
    buyProduct() {
      console.log('buy clicked')
    },
    editProduct() {
      this.$refs.editProductRef.openModal()
    },
    deleteReject() {
      this.loading = false
    },
    deleteProduct() {
      this.loading = true
      this.$refs.deleteConfirmationRef.openModal()
    },
    async deleteProductConfirm(productId) {
      this.deleteProductFromDB(
        {
          url: `/Products/${productId}`,
          method: 'DELETE'
        },
        () => {
          this.loading = false
          this.$emit('product-deleted', productId)
        },
        error => {
          console.error(error)
          this.$toast.error('product delete failed please contact support')
        }
      )
      this.$refs.deleteConfirmationRef.closeModal()
    },
    updateProduct(product) {
      this.updateProductRequest(
        {
          url: `/Products/${product.Id}`,
          method: 'PUT',
          data: product
        },
        () => {
          this.loading = false
          this.$emit('product-updated', product)
        },
        error => {
          console.error(error)
          this.$toast.error('product update failed please contact support')
        }
      )
      this.$refs.editProductRef.closeModal()
    }
  },
  mounted() {},
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
  components: { Like, DeleteConfirmation, EditProduct }
}
</script>
<style lang="scss">
.product {
  position: relative;
  bottom: 0;
  transition: all ease 0.5s !important;
}
.product:hover {
  bottom: 10px;
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2) !important;
}
.pointer {
  cursor: pointer;
}
</style>
