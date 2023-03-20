import  { createRouter, createWebHistory } from 'vue-router'
import WorkShiftList from '@/view/WorkShiftList.vue'
import AddWorkingShift from '@/view/AddWorkingShift.vue'


// createApp(App).mount('#app')

export const routers = [
    {
      path: '/',
      component: WorkShiftList
    },
    {
      path: '/add-working-shift/0',
      component: AddWorkingShift
    },{
      path: '/add-working-shift/:id',
      component: AddWorkingShift
    }
  ]
  const router = createRouter({
    history: createWebHistory(),
    routes: routers,
  })

  export default router;