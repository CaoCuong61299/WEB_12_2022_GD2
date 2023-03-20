<script>
import { ref, toRef, watch } from "vue";
import DxDropDownButton from "devextreme-vue/drop-down-button";
export default {
  components: {
    DxDropDownButton,
  },
  emits: ["onChangePageSide", "totalPageChange"],
  props:  {
    totalPage: Object,
  },
  setup(props, {emit}) {
    
    const pageSide = ["15", "25", "50", "100"];
    const text = ref("15");
    const TotalPage = toRef(props,'totalPage')
    const startRecord = ref(1);
    const endRecord = ref();
    endRecord.value = TotalPage._object.totalPage.CurrentPageRecords;

    watch(TotalPage,()=>{
      startRecord.value =
      TotalPage._object.totalPage.CurrentPage === 1
          ? 1
          : 1 + (TotalPage._object.totalPage.CurrentPage - 1) * TotalPage._object.totalPage.CurrentPageRecords;
      endRecord.value =
      TotalPage._object.totalPage.CurrentPage === TotalPage._object.totalPage.TotalPage
          ? TotalPage._object.totalPage.TotalRecord
          : TotalPage._object.totalPage.CurrentPageRecords * TotalPage._object.totalPage.CurrentPage;
    });
    const onItemClick = (data) =>{
      text.value = data.itemData
      emit("onChangePageSide", parseInt(text.value));
    }
    const backPage = () =>{
      if(TotalPage._object.totalPage.CurrentPage > 1){
        emit("totalPageChange", TotalPage._object.totalPage.CurrentPage - 1);
      }
    }
    const nextPage = () =>{
      if(TotalPage._object.totalPage.CurrentPage < TotalPage._object.totalPage.TotalPage){
        emit("totalPageChange", TotalPage._object.totalPage.CurrentPage + 1);
      }
    }
    return {
      backPage,
      nextPage,
      pageSide,
      text,
      TotalPage,
      onItemClick,
      startRecord,
      endRecord
    };
  },
};
</script>

<template>
  <div class="paging__left">
    Tổng số bảng ghi: <span style="font-weight: bold">{{ TotalPage.TotalRecord }}</span>
  </div>
  <div class="paging__right">
    <div class="paging__right-dropdown">
      <DxDropDownButton
        :text="text"
        :items="pageSide"
        :drop-down-options="{ width: 70 }"
        @item-click="onItemClick"
      />
    </div>
    <div class="paging__right-content">Từ <b>{{ startRecord }}</b> đến <b>{{ endRecord }}</b> bản ghi</div>
    <div class="paging__right-arrow">
      <div class="paging__right-arrow-left" @click="backPage"></div>
      <div class="paging__right-arrow-right" @click="nextPage"></div>
    </div>
  </div>
</template>
<style scoped>
.dx-popup-content {
  background-color: red !important;
  color: #fff !important;
}
.dx-buttongroup {
  width: 70px !important;
  height: 36px !important;
}
.dx-dropdownbutton-action.dx-buttongroup-last-item.dx-button
  .dx-button-content {
  padding-right: 0;
  width: 70px !important;
  height: 36px !important;
}
.dx-buttongroup {
  width: 70px !important;
  height: 36px !important;
}
.dx-buttongroup-wrapper {
  width: 70px !important;
  height: 36px !important;
}
</style>