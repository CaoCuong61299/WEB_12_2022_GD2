import { createApp } from 'vue'
import App from './App.vue'
import 'devextreme/dist/css/dx.light.css';
import router from './router';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import { MISA_ENUM } from './js/base/enums';

const options = {
  transition: "Vue-Toastification__bounce",
  maxToasts: 10,
  newestOnTop: true,
};
const app = createApp(App);
app.config.globalProperties.$MISAEnum = MISA_ENUM;
app.use(Toast, options);
app.use(router)
app.mount("#app");

