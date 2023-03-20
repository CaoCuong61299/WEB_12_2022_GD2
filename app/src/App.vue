<script setup>
import TheHeader from './layout/TheHeader.vue';
import TheMain from './layout/TheMain.vue';
component: [TheHeader,TheMain]
</script>
<template>
  <div class="container">
    <TheHeader></TheHeader>
    <TheMain>
      <router-view></router-view>
    </TheMain>
  </div>
</template>

<style>
@import url(./css/main.css);
</style>
