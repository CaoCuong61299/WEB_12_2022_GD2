<template>
  <div class="tooltip-container">
    <div
      class="con-ms-tooltip"
      :id="id"
      @mouseenter="toggleDefault"
      @mouseleave="toggleDefault"
    >
      <slot></slot>
    </div>
    <DxTooltip
      v-model:visible="defaultVisible"
      :hide-on-outside-click="false"
      :target="`#${id}`"
    >
      {{ text }}
    </DxTooltip>
  </div>
</template>

<script>
import { ref } from "vue";
import { DxTooltip } from "devextreme-vue/tooltip";
export default {
  name: "Mtooltip",
  props: ["id", "text"],
  components: {
    DxTooltip,
  },
  setup() {
    let defaultVisible = ref(false);
    let toggleDefault = () => {
      defaultVisible.value = !defaultVisible.value;
    };
    return {
      defaultVisible,
      toggleDefault,
    };
  },
};
</script>

<style scoped></style>
