<template>
  <div class="products">
    <AddNewProduct :showAddNewModal="showAddNewModal" />
    <v-row>
      <v-col>
        <v-btn
          class="ml-3 mt-7 white--text"
          color="#56896c"
          @click="() => (showAddNewModal = true)"
          >Add new product</v-btn
        >
      </v-col>
      <v-col md="5">
        <v-row>
          <v-col>
            <v-text-field
              v-model="searchProduct"
              label="Search Products"
              class="mt-5 mr-3"
            ></v-text-field>
          </v-col>
          <v-col cols="3">
            <v-btn class="mr-3 mt-7 white--text" color="primary">Search</v-btn>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <div v-if="!loading" class="product-wrapper">
      <Product
        v-for="(product, index) in products"
        :product="product"
        :key="index"
      />
    </div>
    <div v-else class="loadingProgress">
      <v-progress-circular
        indeterminate
        size="60"
        color="#224638"
      ></v-progress-circular>
    </div>
  </div>
</template>
<script>
import product from '@/assets/js/api/product'
import Product from '../../components/products/Product.vue'
import AddNewProduct from '../../components/products/models/AddNewProduct.vue'
export default {
  name: 'Products',
  components: { Product, AddNewProduct },
  mixins: [product],
  data: () => ({
    products: [],
    loading: false,
    searchProduct: '',
    showAddNewModal: false
  }),
  methods: {
    async getAllProducts() {
      this.loading = true
      const data = {
        method: 'GET',
        url: '/Products'
      }
      await this.getProducts(
        data,
        response => {
          this.loading = false
          console.log(response)
          this.products = response.data
        },
        error => {
          this.loading = false
          console.error(error)
        }
      )
    }
  },
  async created() {
    await this.getAllProducts()
  }
}
</script>
<style lang="scss" scoped>
@import '@/assets/styles/variables.scss';
v .loadingProgress {
  display: flex;
  width: 100%;
  min-height: 80vh;
  justify-content: center;
  align-items: center;
}
.product-wrapper {
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start;
}
</style>
