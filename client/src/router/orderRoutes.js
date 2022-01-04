export const orderRoutes = [
  {
    path: '/cart',
    name: 'Cart',
    component: () =>
      import(/*webpackChunkName: "Cart" */ '@/views/Order/CartView.vue'),
    meta: {
      isRequiredAuth: true
    }
  }
]
