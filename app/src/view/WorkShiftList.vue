<script>
import _ from "lodash";
import { DxButton } from "devextreme-vue/button";
import { DxTextBox } from "devextreme-vue/text-box";
import { DxTooltip } from "devextreme-vue/tooltip";
import { DxPopover } from "devextreme-vue/popover";
import { DxPopup, DxPosition, DxToolbarItem } from "devextreme-vue/popup";
import { useToast } from "vue-toastification";
import MFilter from "@/components/MFilter.vue";
import {
  OPTION_TOAST_MESS_SUCCESS,
  OPTION_TOAST_MESS_ERROR,
} from "@/js/constans";
import { MISA_ENUM } from "@/js/base/enums";
import { MISAResource } from "@/js/base/resource";
import "devextreme/dist/css/dx.light.css";
import MPaging from "@/components/MPaging.vue";
import DxDataGrid, {
  DxColumn,
  DxSelection,
  DxPaging,
  DxLoadPanel,
  DxScrolling,
  DxColumnFixing,
  DxColumnChooser,
} from "devextreme-vue/data-grid";
import { DxCheckBox } from "devextreme-vue/check-box";
import { reactive, ref, watch } from "vue";
import DxDropDownButton from "devextreme-vue/drop-down-button";
import APIWorkingShift from "@/Axios/APIWorkingShift.js";
import { APIColumnTable } from "@/Axios/APIColumnTable";
import "devextreme/data/odata/store";
import DxSortable from "devextreme-vue/sortable";
import {
  PAGING_OPTION,
  PROPETY_TABLE,
  PROPETY_FILTER,
  PROPETY_OPERATOR,
  PROPETY_OPERATORNUMBER,
} from "@/js/constans";
import { commonJS } from "@/js/commonJS";
import DxScrollView from "devextreme-vue/scroll-view";
import { DxSelectBox } from "devextreme-vue/select-box";
import { DxDateBox } from "devextreme-vue/date-box";
import { DxNumberBox } from "devextreme-vue/number-box";
export default {
  name: "WorkShiftList",
  components: {
    DxTextBox,
    DxTooltip,
    DxDataGrid,
    DxColumn,
    DxCheckBox,
    DxButton,
    DxPaging,
    DxLoadPanel,
    DxSelection,
    DxDropDownButton,
    DxColumnFixing,
    DxScrolling,
    DxPopup,
    DxPosition,
    DxToolbarItem,
    DxColumnChooser,
    MPaging,
    MFilter,
    DxScrollView,
    DxSortable,
    DxPopover,
    DxSelectBox,
    DxDateBox,
    DxNumberBox,
  },
  setup() {
    //#region Field
    const paging = reactive(PAGING_OPTION);
    const showNavButtons = ref(true);
    const showInfo = ref(true);
    const displayMode = ref("compact");
    const dataSource = ref([]);
    const selectedItem = ref([]);
    const isShowService = ref(false);
    const isLoadding = ref(false);
    const workingShiftFilter = ref();
    const dataTime = ref();
    const isShowPopup = ref(false);
    const idOnClick = ref();
    const TextPopup = ref();
    const isCheckDelete = ref(true);
    const toast = useToast();
    const lstHeaderTable = ref();
    const istable = ref(false);
    const lstHeaderTableRender = ref();
    const lstFilter = ref(_.cloneDeep(PROPETY_FILTER));
    const lstFilterDefault = ref(_.cloneDeep(PROPETY_FILTER));
    const optionFilter = ref(_.cloneDeep(PROPETY_OPERATOR));
    const optionFilterNumber = ref(_.cloneDeep(PROPETY_OPERATORNUMBER));

    //#endregion

    /**
     * Hàm lấy dữ liệu theo phân trang và tìm kiếm
     * @param {Pagesize PageNumber KeyWord} params
     * Author: NCCONG (10/03/2023)
     */
    const getData = async (customParam, params) => {
      try {
        var result = await APIWorkingShift.GetByPagingAndFilter(
          customParam,
          params
        );
        if (result) {
          istable.value = true;
          dataSource.value = result.data;
          setTimeout(() => {
            isLoadding.value = false;
          }, 700);
        }
      } catch (error) {
        toast.success(
          MISAResource.MESS.ERROR_EXCEPTION,
          OPTION_TOAST_MESS_ERROR
        );
      }
    };
    /**
     * gọi hàm get data
     * Author: NCCONG(10/03/2023)
     */
    getData(paging.customParams, paging);
    /**
     * Hàm load lại dữ liệu
     * Author: NCCONG(10/03/2023)
     */
    const refeshData = () => {
      paging.pageNumber = 1;
      isLoadding.value = true;
      getData(paging.customParams, paging);
    };

    /**
     * Hàm theo dõi đối tượng
     * Author: NCCONG(10/03/2023)
     */
    watch(workingShiftFilter, _.debounce((newValue) => {
      paging.workingShiftFilter =
        commonJS.toLowerCaseNonAccentVietnamese(newValue);
      paging.pageNumber = 1;
      paging.workingShiftFilter = newValue;
      setTimeout(getData(paging.customParams, paging))
    },300));

    /**
     * Hàm đổi số bản ghi
     * @param {PageSize số bản ghi} value
     * Author: NCCONG(10/03/2023)
     */
    const onChangePageSide = (value) => {
      paging.pageSize = value;
      paging.pageNumber = 1;
      isLoadding.value = true;
      getData(paging.customParams, paging);
    };

    /**
     * Hàm đổi số trang
     * @param {PageNumber số trang} value
     * Author: NCCONG(10/03/2023)
     */
    const totalPageChange = (value) => {
      paging.pageNumber = value;
      getData(paging.customParams, paging);
    };

    /**
     * Hàm xóa 1 bản ghi
     * @param {WorkingShiftID} value
     * Author: NCCONG(10/03/2023)
     */
    const removerWorkingShift = async (value) => {
      try {
        var result = await APIWorkingShift.deleteOneWorkingShift(value);
        if (result) {
          toast.success(
            MISAResource.MESS.DEL_SUCCESS,
            OPTION_TOAST_MESS_SUCCESS
          );
          isLoadding.value = true;
          getData(paging.customParams, paging);
        }
      } catch (error) {
        toast.success(
          MISAResource.MESS.ERROR_EXCEPTION,
          OPTION_TOAST_MESS_ERROR
        );
      }
    };

    /**
     * Hàm bỏ chọn
     * Author: NCCONG(10/03/2023)
     */
    const handleUnCheck = () => {
      selectedItem.value = null;
    };

    /**
     * Hàm xử lý check box
     * @param {dữ liệu bản ghi đã check} data
     * Author: NCCONG(10/03/2023)
     */
    const handleSelectionChanged = (data) => {
      selectedItem.value = data.selectedRowKeys;
      if (selectedItem.value?.length) {
        isShowService.value = true;
      } else {
        isShowService.value = false;
      }
    };

    /**
     * Hàm xử lý xóa nhiều
     * @param {List WorkingShiftID} params
     * Author: NCCONG(10/03/2023)
     */
    const handleDeleteMany = async (params) => {
      try {
        let result = await APIWorkingShift.deleteManyWorkingShift(params);
        if (result) {
          toast.success(
            MISAResource.MESS.DEL_SUCCESS,
            OPTION_TOAST_MESS_SUCCESS
          );
          isLoadding.value = true;
          getData(paging.customParams, paging);
          isShowService.value = false;
        } else {
        }
      } catch (error) {
        toast.success(
          MISAResource.MESS.ERROR_EXCEPTION,
          OPTION_TOAST_MESS_ERROR
        );
      }
    };

    /**
     * Hàm xóa nhiều
     * Author: NCCONG(10/03/2023)
     */
    const callDeleteMany = () => {
      handleDeleteMany(selectedItem.value);
    };

    /**
     * Hàm show popup xóa 1
     * @param {Id ca làm việc} value
     * Author: NCCONG(10/03/2023)
     */
    const openPopup = (value) => {
      TextPopup.value = "Bạn có chắc chắn muốn xóa ca làm việc này không?";
      isShowPopup.value = true;
      isCheckDelete.value = false;
      idOnClick.value = value;
      console.log(idOnClick.value);
    };

    /**
     * Hàm show popup xóa nhiều
     * Author: NCCONG(10/03/2023)
     */
    const openPopupDel = () => {
      TextPopup.value =
        "Bạn có chắc chắn muốn những xóa ca làm việc này không?";
      isCheckDelete.value = true;
      isShowPopup.value = true;
    };

    /**
     * Hàm ẩn popup xóa
     * Author: NCCONG(10/03/2023)
     */
    const hidePopupDelete = () => {
      isShowPopup.value = false;
    };

    /**
     * Hàm xửa lý xóa
     * Author: NCCONG(10/03/2023)
     */
    const handleDelete = () => {
      if (isCheckDelete.value) {
        isShowPopup.value = false;
        handleDeleteMany(selectedItem.value);
      } else {
        isShowPopup.value = false;
        removerWorkingShift(idOnClick.value);
      }
    };

    /**
     * Hàm export Excel
     * Author: NCCONG(10/03/2023)
     */
    const exportExcel = async () => {
      try {
        await APIWorkingShift.exportExcel().then(function (response) {
          // Lấy ra URL
          const url = URL.createObjectURL(new Blob([response.data]));
          // Tạo thẻ a
          const link = document.createElement("a");
          // Set href của thẻ a là url
          link.href = url;
          // Set attribute của thẻ a và tên của file excel
          link.setAttribute("download", "Danh_sach_ca_lam_viec.xlsx");
          document.body.appendChild(link);
          link.click();
        });
      } catch (error) {
        toast.success(
          MISAResource.MESS.ERROR_EXCEPTION,
          OPTION_TOAST_MESS_ERROR
        );
      }
    };

    /**
     * Hàm lấy dữ liệu setting cột trong bảng
     * Author: NCCONG(11/03/2023)
     */
    const getDataColumnTable = async () => {
      try {
        var result = await APIColumnTable.GetAll();
        if (result) {
          lstHeaderTable.value = _.cloneDeep(result.data);
          lstHeaderTableRender.value = _.cloneDeep(result.data).filter(
            (x) => x.IsCheck != false
          );
        }
      } catch (error) {
        toast.success(
          MISAResource.MESS.ERROR_EXCEPTION,
          OPTION_TOAST_MESS_ERROR
        );
      }
    };
    getDataColumnTable();
    /**
     * Hàm thêm thông tin bảng
     * @param {Thông tin bảng} params
     * Author: NCCONG(11/03/2023) 
     */
    const postColumnTable = async (params) => {
      try {
        var result = await APIColumnTable.PostColumn(params);
      } catch (error) {}
    };

    /**
     * Hàm xóa thông tin cột trong bảng 
     * Author: NCCONG (13/03/2023)
     */
    const deleteColumnTable = async () => {
      try {
        var result = await APIColumnTable.DeleteColumn();
      } catch (error) {}
    };

    /**
     * Hàm điều chỉnh vị trí cột
     * @param {sự kiện} e 
     * Author: NCCONG (14/03/2023)
     */
    const onDragStart = (e) => {
      e.itemData = lstHeaderTable.value[e.fromIndex];
    };
    /**
     * Hàm điều chỉnh vị trí cột
     * @param {sự kiện} e 
     * Author: NCCONG (14/03/2023)
     */
    const onReorder = (e) => {
      lstHeaderTable.value.splice(e.fromIndex, 1);
      lstHeaderTable.value.splice(e.toIndex, 0, e.itemData);
    };

    const isCheckChangeColumn = ref(false);

    /**
     * Hàm đóng tùy chỉnh cột
     * Author: NCCONG(14/03/2023)
     */
    const onCloseColumnTable = () => {
      isCheckChangeColumn.value = !isCheckChangeColumn.value;
    };

    /**
     * Hàm sự kiện xác nhận tùy chỉnh cột
     * Author: NCCONG(15/03/2023)
     */
    const onChangeColumnTable = async () => {
      lstHeaderTable.value.forEach((item, index) => {
        item.OrderColumn = index + 1;
      });
      const lstHeader = lstHeaderTable.value;
      await deleteColumnTable();
      await postColumnTable(lstHeader);
      await getDataColumnTable();
      onCloseColumnTable();
      istable.value = false;
      isLoadding.value = true;
      await getData(paging.customParams, paging);
    };
    const isShowFilter = ref(false);

    /**
     * Hàm Đóng mở filter
     * Author: NCCONG (15/03/2023)
     */
    const onShowFilter = () => {
      isShowFilter.value = !isShowFilter.value;
    };

    /**
     * Hàm xét thông tin cột về mặc định
     * Author: NCCONG (15/03/2023)
     */
    const onDefaultColumnTable = async () => {
      await deleteColumnTable();
      await postColumnTable(_.cloneDeep(PROPETY_TABLE));
      await getDataColumnTable();
      onCloseColumnTable();
      isLoadding.value = true;
      istable.value = false;
      await getData(paging.customParams, paging);
    };
    const isShowFilterMessage = ref(false);
    const lstFilterMessage = ref();

    /**
     * Hàm xác nhận filter
     * Author: NCCONG (16/03/2023)
     */
    const onFilter = async () => {
      paging.customParams = lstFilter.value
        .filter((x) => x.isCheck != false)
        .map((item) => {
          return {
            ValueField: item.ValueField,
            InputValue: String(item.value),
            Operator: item.operation,
          };
        });
      lstFilterMessage.value = _.cloneDeep(
        lstFilter.value.filter((x) => x.isCheck == true)
      );
      if (paging.customParams.length > 0) {
        isShowFilterMessage.value = true;
      } else {
        isShowFilterMessage.value = false;
      }
      await filterMessage(lstFilterMessage.value);
      isLoadding.value = true;
      istable.value = false;
      paging.pageNumber = 1;
      await getData(paging.customParams, paging);
    };
    const isShowBtnFilter = ref(false);
    const nameFilterMessage = ref();
    const operationMessage = ref();
    const valueMessage = ref();
    /**
     * Hàm hiển thị message filter
     * @param {Thông tin bản ghi} params 
     * Author: NCCONG(16/03/2023)
     */
    const filterMessage = async (params) => {
      var operationMsg = "";
      var valueMsg;
      nameFilterMessage.value = params.map((item) => {
        operationMsg =
          item.operation == "contains"
            ? "Chứa"
            : item.operation == "notcontains"
            ? "Không Chứa"
            : item.operation == "equals"
            ? "Bằng"
            : item.operation == "other"
            ? "Khác"
            : item.operation == "greater"
            ? "Lớn hơn"
            : "Nhơ hơn";
        valueMsg = !item.value ? "Rỗng" : item.value;
        return {
          key: item.ValueField,
          nameMap: item.Caption,
          operationMap: operationMsg,
          value: valueMsg,
        };
      });
    };

    /**
     * Hàm xét filter về default
     * Author: NCCONG (16/03/2023)
     */
    const onFilterDefaul = async () => {
      paging.customParams = [];
      paging.pageNumber = 1;
      lstFilter.value = _.cloneDeep(PROPETY_FILTER);
      lstFilterMessage.value = _.cloneDeep(
        lstFilter.value.filter((x) => x.isCheck == true)
      );
      if (paging.customParams.length > 4) {
        isShowBtnFilter.value = true;
      } else {
        isShowBtnFilter.value = false;
      }
      if (paging.customParams.length > 0) {
        isShowFilterMessage.value = true;
      } else {
        isShowFilterMessage.value = false;
      }
      await filterMessage(lstFilterMessage.value);
      isShowFilter.value = false;
      isLoadding.value = true;
      istable.value = false;
      await getData(paging.customParams, paging);
    };

    /**
     * Hàm bỏ chọn trực tiếp trên filter
     * @param {Key} param 
     * Author: NCCONG (18/03/2023)
     */
    const unCheckFilterItem = async (param) => {
      lstFilter.value.filter((x)=>x.ValueField == param).map((item)=> item.isCheck = false)
      lstFilterMessage.value = _.cloneDeep(
        lstFilter.value.filter((x) => x.isCheck == true)
      );
      await filterMessage(lstFilterMessage.value);
      paging.customParams = lstFilter.value
        .filter((x) => x.isCheck != false)
        .map((item) => {
          return {
            ValueField: item.ValueField,
            InputValue: String(item.value),
            Operator: item.operation,
          };
        });
      if (paging.customParams.length > 0) {
        isShowFilterMessage.value = true;
      } else {
        isShowFilterMessage.value = false;
      }
      isShowFilter.value = false;
      isLoadding.value = true;
      istable.value = false;
      await getData(paging.customParams, paging);

    };
    return {
      paging,
      dataSource,
      showInfo,
      displayMode,
      showNavButtons,
      onChangePageSide,
      totalPageChange,
      removerWorkingShift,
      handleUnCheck,
      selectedItem,
      handleSelectionChanged,
      isShowService,
      handleDeleteMany,
      callDeleteMany,
      isLoadding,
      refeshData,
      workingShiftFilter,
      dataTime,
      isShowPopup,
      hidePopupDelete,
      handleDelete,
      openPopup,
      idOnClick,
      TextPopup,
      isCheckDelete,
      openPopupDel,
      exportExcel,
      toast,
      lstHeaderTableRender,
      lstHeaderTable,
      onReorder,
      onDragStart,
      onChangeColumnTable,
      isCheckChangeColumn,
      onCloseColumnTable,
      onDefaultColumnTable,
      onShowFilter,
      isShowFilter,
      postColumnTable,
      deleteColumnTable,
      istable,
      lstFilter,
      optionFilter,
      optionFilterNumber,
      onFilter,
      onFilterDefaul,
      lstFilterDefault,
      filterMessage,
      lstFilterMessage,
      isShowFilterMessage,
      nameFilterMessage,
      operationMessage,
      valueMessage,
      unCheckFilterItem,
      isShowBtnFilter,
    };
  },
};
</script>
<template>
  <div class="content__top">
    <div class="content__top-left">Ca làm việc</div>
    <div class="content-top-right">
      <router-link to="/add-working-shift/0">
        <DxButton
          text="Thêm"
          icon="add"
          class="btn"
          id="test"
          style="
            width: 100px !important;
            padding: 0px 16px 0px 12px !important;
            border-radius: 4px 0px 0px 4px !important;
          "
        ></DxButton>
      </router-link>
      <div class="borderRight"></div>
      <DxButton
        icon="chevrondown"
        class="btn"
        style="
          border-radius: 0px 4px 4px 0px !important;
          padding: 6px 5px !important;
          width: 36px !important;
        "
      ></DxButton>
    </div>
  </div>
  <div class="content__filter-message" v-if="isShowFilterMessage">
    <div class="filter__message-left">
      <div
        class="filter__item-message"
        v-for="item in nameFilterMessage"
        :key="item"
      >
        <div class="filter__message-text">
          <b style="margin-right: 3px">{{ item.nameMap }}</b>
          {{ item.operationMap }}
          <b style="margin-left: 3px">{{ item.value }}</b>
        </div>
        <div class="filter__message-icon" @click="unCheckFilterItem(item.key)">
          <div class="icon"></div>
        </div>
      </div>
    </div>
    <div class="filter__message-right">
      <div class="filter__message-ci">
        <div class="filter__message-btn" v-if="isShowBtnFilter">
          <div class="circle"></div>
          <div class="circle"></div>
          <div class="circle"></div>
        </div>
      </div>
      <div class="filter__message-uncheck" @click="onFilterDefaul">
        Xóa tất cả
      </div>
    </div>
  </div>
  <div
    class="content__center"
    :style="{
      height: isShowFilterMessage
        ? 'calc(100% - 52px - 48px)'
        : 'calc(100% - 52px)',
    }"
  >
    <div
      class="content__center-full"
      :style="{
        width: isShowFilter ? 'calc(100% - 260px)' : '100%',
      }"
    >
      <div class="content__center-top">
        <div class="content__center-top-left">
          <div class="input__seach" v-if="!isShowService">
            <input
              type="text"
              v-model="workingShiftFilter"
              :placeholder="'Tìm kiếm'"
              class="input"
            />
            <div class="icon-seach"></div>
          </div>
          <div class="sevice__many" v-if="isShowService">
            <div class="sevice__text">
              Đã chọn <b>{{ selectedItem?.length }}</b>
            </div>
            <div class="sevice__button">
              <button class="btn_uncheck" @click="handleUnCheck">
                Bỏ Chọn
              </button>
              <button class="btn_deleteall" @click="openPopupDel">
                <div class="icon_delete"></div>
                <div class="text_delete">Xóa</div>
              </button>
            </div>
          </div>
        </div>
        <div class="content__center-top-right">
          <div class="right-button" id="btn_loadData" @click="refeshData">
            <div class="right-button-loaddata"></div>
          </div>
          <div class="right-button" id="btn_filter" @click="onShowFilter">
            <div id="btn_filter" class="right-button-filter"></div>
          </div>
          <div class="right-button" id="btn_out" @click="exportExcel">
            <div class="right-button-out"></div>
          </div>
          <div
            class="right-button"
            id="btn_setting"
            @click="onCloseColumnTable"
          >
            <div class="right-button-setting"></div>
          </div>
        </div>
      </div>
      <div>
        <DxTooltip
          target="#btn_loadData"
          show-event="dxhoverstart"
          hide-event="dxhoverend"
        >
          Tải lại dữ liệu
        </DxTooltip>
        <DxTooltip
          target="#btn_filter"
          show-event="dxhoverstart"
          hide-event="dxhoverend"
        >
          Bộ lọc
        </DxTooltip>
        <DxTooltip
          target="#btn_setting"
          show-event="dxhoverstart"
          hide-event="dxhoverend"
        >
          Tùy chỉnh cột
        </DxTooltip>
        <DxTooltip
          target="#btn_out"
          show-event="dxhoverstart"
          hide-event="dxhoverend"
        >
          Xuất khẩu
        </DxTooltip>
      </div>
      <DxPopover
        target="#btn_setting"
        :width="350"
        :height="390"
        :visible="isCheckChangeColumn"
      >
        <div class="customizeColumns">
          <div class="customizeColumns__header">
            <div class="customizeColumns__header-row">
              <div class="customizeColumns__header-title">Tùy chỉnh cột</div>
              <div
                class="customizeColumns__header-close"
                @click="onCloseColumnTable"
              >
                <div class="customizeColumns__header-icon"></div>
              </div>
            </div>
            <div class="customizeColumns__header-row">
              
            </div>
          </div>
          <div class="customizeColumns__content">
            <DxScrollView
              id="scroll"
              style="width: 100%; height: 100%"
              show-scrollbar="always"
              direction="vertical"
            >
              <DxSortable
                id="list"
                drop-feedback-mode="push"
                item-orientation="vertical"
                drag-direction="both"
                @drag-start="onDragStart"
                @reorder="onReorder"
              >
                <template #content>
                  <div
                    class="list-group"
                    v-for="item in lstHeaderTable"
                    :key="item.Field"
                  >
                    <div class="list-group_prop">
                      <div class="listCheckbox">
                        <DxCheckBox v-model:value="item.IsCheck"></DxCheckBox>
                      </div>
                      <div class="columnName" :title="item.Caption">
                        {{ item.Caption }}
                      </div>
                    </div>
                    <div class="list-group_icon">
                      <div class="icon-list"></div>
                    </div>
                  </div>
                </template>
              </DxSortable>
            </DxScrollView>
          </div>
          <div class="customizeColumns__footer">
            <DxButton
              text="Mặc định"
              class="btn btn_white"
              style="
                width: 90px !important;
                padding: 0px 16px 0px 12px !important;
                border-radius: 4px !important;
                margin-right: 12px;
              "
              @click="onDefaultColumnTable"
            ></DxButton>
            <DxButton
              text="Lưu"
              class="btn"
              style="
                width: 90px !important;
                padding: 0px 16px 0px 12px !important;
                border-radius: 4px !important;
              "
              @click="onChangeColumnTable"
            ></DxButton>
          </div>
        </div>
      </DxPopover>

      <div class="content__center-table" v-if="istable">
        <DxDataGrid
          :data-source="dataSource.Data"
          key-expr="WorkingShiftId"
          :remote-operations="false"
          :allow-column-reordering="true"
          :allow-column-resizing="true"
          :hover-state-enabled="true"
          style="height: 100% !important"
          :selected-row-keys="selectedItem"
          @selection-changed="handleSelectionChanged"
          noDataText="Không có dữ liệu"
        >
          <DxColumnFixing :enabled="true"> </DxColumnFixing>
          <!-- Check Box -->
          <DxSelection
            mode="multiple"
            select-all-mode="page"
            showCheckBoxesMode="always"
          />
          <DxColumn
            v-for="item in lstHeaderTableRender"
            :key="item.Field"
            :dataField="item.Field"
            :caption="item.Caption"
            :width="item.Width"
            :fixed="item.Fixed"
            :alignment="item.Alignemnt"
            :data-type="item.DataType"
            :format="item.Format"
          ></DxColumn>
          <DxColumn
            :width="50"
            cell-template="btn_edit"
            data-field="WorkingShiftId"
            caption=""
            :fixed="true"
            fixed-position="right"
            :show-in-column-chooser="false"
          >
          </DxColumn>
          <DxColumn
            :width="50"
            cell-template="btn_delete"
            data-field="WorkingShiftId"
            caption=""
            :fixed="true"
            fixed-position="right"
            :show-in-column-chooser="false"
          >
          </DxColumn>
          <template #btn_edit="{ data }">
            <router-link :to="`/add-working-shift/${data.value}`">
              <div class="btn_edit">
                <div class="btn_edit-icon"></div>
              </div>
            </router-link>
          </template>
          <template #btn_delete="{ data }">
            <div class="btn_delete" @click="openPopup(data.value)">
              <div class="btn_delete-icon"></div>
            </div>
          </template>
          <DxPaging :enabled="false" />
          <DxLoadPanel :enabled="false" />
        </DxDataGrid>
      </div>
      <div class="content__center-paging">
        <MPaging
          v-bind:totalPage="dataSource"
          @onChangePageSide="onChangePageSide"
          @totalPageChange="totalPageChange"
        ></MPaging>
      </div>
    </div>
    <div class="content__center-filter" v-if="isShowFilter">
      <div class="filter-header">
        <div class="filter-header-row">
          <div class="filter-header-text">Bộ lọc</div>
          <div class="filter-header-icon" @click="onShowFilter">
            <div class="icon"></div>
          </div>
        </div>
        <div class="filter-header-row">
          
        </div>
      </div>
      <div class="filter-content">
        <DxScrollView
          id="scroll"
          style="width: 100%; height: 100%"
          show-scrollbar="always"
          direction="vertical"
        >
          <div
            class="filter-content-row"
            v-for="(item, index) in lstFilter"
            :key="index"
            :style="{ height: item.isCheck ? '126px' : '30px' }"
            :class="item.isCheck ? 'color-1' : 'color-2'"
          >
            <div class="row-filter" style="margin-left: 8px">
              <DxCheckBox v-model:value="item.isCheck"></DxCheckBox>
              <span class="caption-filter">{{ item.Caption }}</span>
            </div>
            <div class="row-filter" v-if="item.isCheck && item.type == ''">
              <DxSelectBox
                v-model:value="item.operation"
                :search-enabled="true"
                noDataText="Không có dữ liệu"
                :data-source="optionFilter"
                display-expr="Name"
                value-expr="operation"
                drop-down-button-template="imageIcon"
              >
                <template #imageIcon="{}">
                  <div class="custom-icon-class">
                    <div class="custom-icon"></div>
                  </div>
                </template>
              </DxSelectBox>
            </div>
            <div class="row-filter" v-if="item.isCheck && item.type == ''">
              <DxTextBox width="100%" v-model:value="item.value"></DxTextBox>
            </div>
            <div
              class="row-filter"
              v-if="
                item.isCheck && (item.type == 'date' || item.type == 'number')
              "
            >
              <DxSelectBox
                v-model="item.operation"
                :search-enabled="true"
                noDataText="Không có dữ liệu"
                :data-source="optionFilterNumber"
                display-expr="Name"
                value-expr="operation"
                icon="chevrondown"
                drop-down-button-template="imageIcon"
              >
                <template #imageIcon="{}">
                  <div class="custom-icon-class">
                    <div class="custom-icon"></div>
                  </div>
                </template>
              </DxSelectBox>
            </div>

            <div class="row-filter" v-if="item.isCheck && item.type == 'date'">
              <DxDateBox
                type="time"
                display-format="HH:mm"
                placeholder="HH:mm"
                style="width: 100%"
                dateSerializationFormat="HH:mm:ss"
                v-model:value="item.value"
              ></DxDateBox>
            </div>
            <div
              class="row-filter"
              v-if="item.isCheck && item.type == 'number'"
            >
              <DxNumberBox
                :min="0"
                :max="24"
                :show-spin-buttons="true"
                format="#0"
                style="width: 100%; text-align: right !important"
                v-model:value="item.value"
                icons="{ spinUp: 'dx-icon-my-custom-icon' }"
              >
              </DxNumberBox>
            </div>
          </div>
        </DxScrollView>
      </div>
      <div class="filter-footer">
        <DxButton
          text="Bỏ lọc"
          class="btn btn_white"
          style="
            width: 90px !important;
            padding: 0px 16px 0px 12px !important;
            border-radius: 4px !important;
            margin-right: 12px;
          "
          @click="onFilterDefaul"
        ></DxButton>
        <DxButton
          text="Áp dụng"
          class="btn"
          style="
            width: 90px !important;
            padding: 0px 16px 0px 12px !important;
            border-radius: 4px !important;
          "
          @click="onFilter"
        ></DxButton>
      </div>
    </div>
  </div>
  <div class="loadding" v-if="isLoadding">
    <div class="svg__icon">
      <img src="../assets/loadding.svg" alt="" />
    </div>
  </div>
  <DxPopup
    v-model:visible="isShowPopup"
    :width="415"
    :height="190"
    :hide-on-outside-click="false"
    :drag-enabled="false"
    :show-close-button="false"
    position="center"
  >
    <div class="popup">
      <div class="popupHeader">
        <div class="popupHeader-text">Cảnh báo</div>
        <div class="popupHeader-right">
          <div class="popupHeader-icon" @click="hidePopupDelete"></div>
        </div>
      </div>
      <div class="popupContent">
        <span class="popupText">{{ TextPopup }}</span>
      </div>
      <div class="popupFooter">
        <DxButton
          class="btn_Cancel btn_white"
          text="Hủy"
          style="
            border-radius: 4px !important;
            padding: 6px 5px !important;
            width: 80px !important;
            margin-right: 16px;
          "
          @click="hidePopupDelete"
        ></DxButton>
        <DxButton
          class="btn btn_red"
          text="Xóa"
          style="
            border-radius: 4px !important;
            padding: 6px 5px !important;
            width: 80px !important;
          "
          @click="handleDelete"
        ></DxButton>
      </div>
    </div>
  </DxPopup>
</template>
<style scoped>
</style>