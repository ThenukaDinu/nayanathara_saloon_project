export const signRoutes = [
  {
    path: '/SignIn',
    name: 'Sign In',
    component: () => import(/* webpackChunkName: "signIn" */ '@/views/SignIn'),
    meta: {
      isRequiredAuth: false
    }
  },
  {
    path: '/SignUp',
    name: 'Sign Up',
    component: () => import(/* webpackChunkName: "signUp" */ '@/views/SignUp'),
    meta: {
      isRequiredAuth: false
    }
  }
]
