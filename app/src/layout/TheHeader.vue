<script>
import MTooltip from "@/components/MTooltip.vue";
import { DxTooltip } from "devextreme-vue/tooltip";
export default {
  components: { MTooltip, DxTooltip },
  setup() {

  },
};
</script>


<template>
  <div class="header">
    <div class="header__left">
      <div class="header__left-menu"></div>
      <div class="header__left-icon"></div>
      <div class="header__left-text">Chấm công</div>
    </div>
    <div class="header__center">
      <div class="header__center-menu">Tổng quan</div>
      <div class="header__center-menu">
        Chấm công
        <div class="header__center-dropdown"></div>
      </div>
      <div class="header__center-menu header__center-active">
        Ca làm việc
        <div class="header__center-dropdown"></div>
      </div>
      <div class="header__center-menu">
        Quản lý đơn
        <div class="header__center-dropdown"></div>
      </div>
      <div class="header__center-menu">
        Báo cáo
        <div class="header__center-dropdown"></div>
      </div>
      <div class="header__center-menu">Thiết Lập</div>
    </div>
    <div class="header__right">
      <div class="header__right-notification" id="notification-icon">
        <div class="header__right-icon"></div>
      </div>

      <div class="header__right-helper" id="helper-icon">
        <div class="header__right-icon"></div>
      </div>
      <div class="header__right-sevice" id="sevice-icon">
        <div class="header__right-icon"></div>
      </div>
      <div class="header__right-avatar"></div>
    </div>
  </div>
  <DxTooltip
    target="#notification-icon"
    show-event="dxhoverstart"
    hide-event="dxhoverend"
  >
    Thông báo
  </DxTooltip>
  <DxTooltip
    target="#helper-icon"
    show-event="dxhoverstart"
    hide-event="dxhoverend"
  >
    Trợ giúp
  </DxTooltip>
  <DxTooltip
    target="#sevice-icon"
    show-event="dxhoverstart"
    hide-event="dxhoverend"
  >
    Tính năng khác
  </DxTooltip>
</template>