<script>
import _ from "lodash";
import { useRoute } from "vue-router";
import router from "@/router";
import { useToast } from "vue-toastification";
import { DxButton } from "devextreme-vue/button";
import { DxTextBox } from "devextreme-vue/text-box";
import APIWorkingShift from "@/Axios/APIWorkingShift";
import { DxDateBox } from "devextreme-vue/date-box";
import { DxCheckBox } from "devextreme-vue/check-box";
import { DxScrollView } from "devextreme-vue/scroll-view";
import { DxNumberBox } from "devextreme-vue/number-box";
import { MISA_ENUM } from "@/js/base/enums";
import { MISAResource } from "@/js/base/resource";
import { DxPopup, DxPosition, DxToolbarItem } from "devextreme-vue/popup";
import {
  DATATYPENAME_TIME,
  OPTION_TOAST_MESS,
  OPTION_TOAST_MESS_ERROR,
  OPTION_TOAST_MESS_SUCCESS,
  DEFAULT_VALUE,
} from "@/js/constans";
import DxValidator, {
  DxRequiredRule,
  DxValidationRule,
} from "devextreme-vue/validator";
import { ref, watch, reactive } from "vue";

export default {
  components: {
    DxButton,
    DxTextBox,
    DxDateBox,
    DxCheckBox,
    DxScrollView,
    DxNumberBox,
    DxValidationRule,
    DxValidator,
    DxRequiredRule,
    DxPopup,
  },
  setup() {
    const onFormSubmit = async (e) => {
      console.log(e);
      e.preventDefault();
    };
    const route = useRoute();
    const isShowStartTime = ref(true);
    const isShowEndTime = ref(true);
    const isShowFreeTime = ref(false);
    const isShowBreakStartTimeBetweenShift = ref(false);
    const isShowNoStartTime = ref(false);
    const isShowNoEndTime = ref(false);
    const hour = ref(0);
    const minute = ref(0);
    const second = ref(0);
    const isCheckValueChange = ref(false);
    const ShiftStartTime = ref();
    const TimekeepingShiftStart = ref();
    const TimekeepingShiftEnd = ref();
    const ShiftEndTime = ref();
    const TimekeepingOutShiftStart = ref();
    const TimekeepingOutShiftEnd = ref();
    const BreakTimeBetweenShiftStart = ref();
    const TimekeepingBreakTimeBetweenShifts = ref();
    const BreakTimeBetweenShiftEnd = ref();
    const BreakTimeBetweenShift = ref();
    const WorkingHour = ref(9);
    const DataName = reactive(DATATYPENAME_TIME);
    const NameDate = ref();
    const toast = useToast();
    const isShowPopup = ref(false);
    //#region Validate

    const isShowValiadateShiftTime1 = ref(false);
    const isShowValiadateShiftTime2 = ref(false);
    const isShowValiadateShiftTime3 = ref(false);
    const isShowValiadateShiftTime4 = ref(false);
    const isShowValiadateShiftTime5 = ref(false);
    const isShowValiadateShiftTime6 = ref(false);
    const isShowValiadateShiftTime7 = ref(false);
    const isShowValiadateShiftTime8 = ref(false);
    const isShowValiadateShiftTime9 = ref(false);
    const isShowValiadateShiftTime10 = ref(false);
    //#endregion
    const titleForm = ref();
    const workingShiftsDefaul = reactive(_.cloneDeep(DEFAULT_VALUE));
    const isShowValiadateName = ref(false);

    const workingShifts = reactive(_.cloneDeep(DEFAULT_VALUE));
    /**
     * Hàm theo dõi biến
     * Author: NCCONG(09/03/2023)
     */
    watch(
      () => isShowStartTime.value,
      (nV) => {
        if (nV === false) {
          isShowEndTime.value = false;
          TimekeepingShiftEnd.value = null;
          TimekeepingShiftStart.value = null;
        }
      }
    );
    /**
     * Hàm theo dõi biến
     * Author: NCCONG(09/03/2023)
     */
    watch(
      () => isShowEndTime.value,
      (nV) => {
        if (nV === true) {
          isShowStartTime.value = true;
          TimekeepingOutShiftEnd.value = null;
          TimekeepingOutShiftStart.value = null;
        }
      }
    );
    /**
     * Hàm theo dõi biến
     * Author: NCCONG(09/03/2023)
     */
    watch(
      () => isShowFreeTime.value,
      (nV) => {
        if (nV === false) {
          TimekeepingBreakTimeBetweenShifts.value = null;
          BreakTimeBetweenShiftStart.value = null;
          isShowBreakStartTimeBetweenShift.value = false;
          BreakTimeBetweenShiftEnd.value = null;
          BreakTimeBetweenShift.value = null;
        }
      }
    );
    watch(workingShifts, (newValue) => {
      console.log(newValue);
      if (JSON.stringify(newValue) === JSON.stringify(workingShiftsDefaul)) {
        isCheckValueChange.value = true;
      } else {
        isCheckValueChange.value = false;
      }
    });
    /**
     * Hàm lấy dữ liệu theo id
     * Author: NCCONG(09/03/2023)
     */
    const getData = async (params) => {
      if (params) {
        var result = await APIWorkingShift.GetByWorkingShiftId(params);
        if (result) {
          titleForm.value = "Sửa " + result.data.WorkingShiftName;
          TimekeepingShiftStart.value = result.data.TimekeepingShiftStart;
          ShiftStartTime.value = result.data.ShiftStartTime;
          TimekeepingShiftEnd.value = result.data.TimekeepingShiftEnd;
          ShiftEndTime.value = result.data.ShiftEndTime;
          TimekeepingOutShiftStart.value = result.data.TimekeepingOutShiftStart;
          TimekeepingOutShiftEnd.value = result.data.TimekeepingOutShiftEnd;
          BreakTimeBetweenShiftEnd.value = result.data.BreakTimeBetweenShiftEnd;
          BreakTimeBetweenShift.value = result.data.BreakTimeBetweenShift;
          BreakTimeBetweenShiftStart.value =
            result.data.BreakTimeBetweenShiftStart;
          TimekeepingBreakTimeBetweenShifts.value =
            result.data.TimekeepingBreakTimeBetweenShifts;
          if (BreakTimeBetweenShiftStart.value != null) {
            isShowFreeTime.value = true;
          }

          if (TimekeepingBreakTimeBetweenShifts.value != null) {
            isShowBreakStartTimeBetweenShift.value = true;
          }

          Object.assign(workingShiftsDefaul, result.data);
          Object.assign(workingShifts, result.data);
          WorkingHour.value = result.data.WorkingHour;
        }
      } else {
        titleForm.value = "Thêm mới ca công việc";
        Object.assign(workingShifts, workingShiftsDefaul);
        isCheckValueChange.value = true;
        ShiftStartTime.value = workingShiftsDefaul.ShiftStartTime;
        TimekeepingShiftStart.value = workingShiftsDefaul.TimekeepingShiftStart;
        TimekeepingShiftEnd.value = workingShiftsDefaul.TimekeepingShiftEnd;
        ShiftEndTime.value = workingShiftsDefaul.ShiftEndTime;
        TimekeepingOutShiftStart.value =
          workingShiftsDefaul.TimekeepingOutShiftStart;
        TimekeepingOutShiftEnd.value =
          workingShiftsDefaul.TimekeepingOutShiftEnd;
      }
    };
    getData(route.params.id);
    const handleSubmitForm = async () => {
      isShowPopup.value = false;
      if (!validateData()) {
        if (route.params.id) {
          try {
            var result = await APIWorkingShift.PutWorkingShift(
              route.params.id,
              workingShifts
            );
            if (result) {
              router.push({ path: "/" });
              toast.success(
                MISAResource.MESS.UPDATE_SUCCESS,
                OPTION_TOAST_MESS_SUCCESS
              );
            }
          } catch (error) {
            if (
              error.response.data.MoreInfo.ErrorCode ===
              MISA_ENUM.ERROR_CODE.DUPLICATECODE
            ) {
              toast.error(
                MISAResource.MESS.DUPLICATECODE,
                OPTION_TOAST_MESS_ERROR
              );
            }
          }
        } else {
          try {
            var result = await APIWorkingShift.PostWorkingShift(workingShifts);
            if (result) {
              router.push({ path: "/" });
              toast.success(
                MISAResource.MESS.ADD_SUCCESS,
                OPTION_TOAST_MESS_SUCCESS
              );
            }
          } catch (error) {
            if (
              error.response.data.MoreInfo.ErrorCode ===
              MISA_ENUM.ERROR_CODE.DUPLICATECODE
            ) {
              toast.error(
                MISAResource.MESS.DUPLICATECODE,
                OPTION_TOAST_MESS_ERROR
              );
            }
          }
        }
      }
    };

    /**
     * Hàm chuyển format dữ liệu ngày tháng
     * Author: NCCONG(09/03/2023)
     */
    const convertTime = (e, name) => {
      if (e) {
        workingShifts[name] = e.value;
        if (!isCheckValueChange.value) {
          if (
            name === "ShiftStartTime" ||
            name === "ShiftEndTime" ||
            name === "BreakTimeBetweenShiftStart" ||
            name === "BreakTimeBetweenShiftEnd"
          ) {
            handleWorkingHouse(
              ShiftStartTime.value,
              ShiftEndTime.value,
              BreakTimeBetweenShiftStart.value,
              BreakTimeBetweenShiftEnd.value
            );
          }
        }
        if (name == "ShiftStartTime") {
          if (e.value === null) {
            isShowValiadateShiftTime1.value = true;
          } else {
            isShowValiadateShiftTime1.value = false;
          }
        }
        if (name == "TimekeepingShiftStart") {
          if (e.value === null) {
            isShowValiadateShiftTime2.value = true;
          } else {
            isShowValiadateShiftTime2.value = false;
          }
        }
        if (name == "TimekeepingShiftEnd") {
          if (e.value === null) {
            isShowValiadateShiftTime3.value = true;
          } else {
            isShowValiadateShiftTime3.value = false;
          }
        }
        if (name === "ShiftEndTime") {
          if (e.value === null) {
            isShowValiadateShiftTime4.value = true;
          } else {
            isShowValiadateShiftTime4.value = false;
          }
        }
        if (name == "TimekeepingOutShiftStart") {
          if (e.value === null) {
            isShowValiadateShiftTime5.value = true;
          } else {
            isShowValiadateShiftTime5.value = false;
          }
        }
        if (name == "TimekeepingOutShiftEnd") {
          if (e.value === null) {
            isShowValiadateShiftTime6.value = true;
          } else {
            isShowValiadateShiftTime6.value = false;
          }
        }
        if (name == "BreakTimeBetweenShiftStart") {
          if (e.value === null) {
            isShowValiadateShiftTime7.value = true;
            isCheckValidateTime7.value = true;
          } else {
            isShowValiadateShiftTime7.value = false;
          }
        }
        if (name === "TimekeepingBreakTimeBetweenShifts") {
          if (e.value === null) {
            isShowValiadateShiftTime8.value = true;
          } else {
            isShowValiadateShiftTime8.value = false;
          }
        }
        if (name == "BreakTimeBetweenShiftEnd") {
          if (e.value === null) {
            isShowValiadateShiftTime9.value = true;
          } else {
            isShowValiadateShiftTime9.value = false;
          }
        }
        if (name == "BreakTimeBetweenShift") {
          if (e.value === null) {
            isShowValiadateShiftTime10.value = true;
          } else {
            isShowValiadateShiftTime10.value = false;
          }
        }
      }
      // Chuỗi ban đầu có định dạng "dd/MM/yyyy hh:mm:ss"
    };

    /**
     * Hàm format dữ liệu số
     * Author: NCCONG(09/03/2023)
     */
    const convertDouble = (e, name) => {
      if (e.value === undefined) {
        workingShifts.WorkingHour = 9.5;
        WorkingHour.value = 9.5;
      }
      workingShifts[name] = e.value;
    };

    /**
     * Hàm format dữ liệu số thành giờ
     * Author: NCCONG(12/03/2023)
     */
    const convertToDecimalHours = (value) => {
      if (value) {
        const timeParts = value.split(":");
        const hours = parseInt(timeParts[0], 10);
        const minutes = parseInt(timeParts[1], 10);
        const decimalHours = hours + minutes / 60;
        return decimalHours;
      }
    };

    /**
     * Hàm tính toán thời gian
     * Author: NCCONG(12/03/2023)
     */
    const handleWorkingHouse = (a, b, c, d) => {
      let decimalA = convertToDecimalHours(a);
      let decimalB = convertToDecimalHours(b);
      let decimalC = convertToDecimalHours(c);
      let decimalD = convertToDecimalHours(d);
      if (decimalC == undefined || decimalD == undefined) {
        WorkingHour.value = decimalB - decimalA;
      } else {
        WorkingHour.value = decimalB - decimalA - (decimalD - decimalC);
      }
    };
    const validateWorkingShiftName = (e) => {
      if (e.value == "") {
        isShowValiadateName.value = true;
      } else {
        isShowValiadateName.value = false;
      }
    };
    const validateWorkingShiftCode = (e) => {
      if (e.value == "") {
        isShowValiadateCode.value = true;
      } else {
        isShowValiadateCode.value = false;
      }
    };
    const isShowValiadateCode = ref(false);
    const validateData = () => {
      var ischeck = true;
      if (workingShifts.WorkingShiftName === "") {
        isShowValiadateName.value = true;
      } else {
        isShowValiadateName.value = false;
      }
      if (workingShifts.WorkingShiftCode == "") {
        isShowValiadateCode.value = true;
      } else {
        isShowValiadateCode.value = false;
      }
      if (!ShiftStartTime.value) {
        isShowValiadateShiftTime1.value = true;
      } else {
        isShowValiadateShiftTime1.value = false;
      }
      if (isShowStartTime.value === true) {
        if (!TimekeepingShiftStart.value) {
          isShowValiadateShiftTime2.value = true;
        } else {
          isShowValiadateShiftTime2.value = false;
        }
        if (!TimekeepingShiftEnd.value) {
          isShowValiadateShiftTime3.value = true;
        } else {
          isShowValiadateShiftTime3.value = false;
        }
      }
      if (!ShiftEndTime.value) {
        isShowValiadateShiftTime4.value = true;
      } else {
        isShowValiadateShiftTime4.value = false;
      }
      if (isShowEndTime.value) {
        if (!TimekeepingOutShiftStart.value) {
          isShowValiadateShiftTime5.value = true;
        } else {
          isShowValiadateShiftTime5.value = false;
        }

        if (!TimekeepingOutShiftEnd.value) {
          isShowValiadateShiftTime6.value = true;
        } else {
          isShowValiadateShiftTime6.value = false;
        }
      }
      if (isShowFreeTime.value === true) {
        if (!BreakTimeBetweenShiftStart.value) {
          isShowValiadateShiftTime7.value = true;
        } else {
          isShowValiadateShiftTime7.value = false;
        }
        if (!BreakTimeBetweenShiftEnd.value) {
          isShowValiadateShiftTime9.value = true;
        } else {
          isShowValiadateShiftTime9.value = false;
        }
        if (isShowBreakStartTimeBetweenShift.value == true) {
          if (!BreakTimeBetweenShift.value) {
            isShowValiadateShiftTime10.value = true;
          } else {
            isShowValiadateShiftTime10.value = false;
          }
          if (!TimekeepingBreakTimeBetweenShifts.value) {
            isShowValiadateShiftTime8.value = true;
          } else {
            isShowValiadateShiftTime8.value = false;
          }
        }
      }

      return (
        isShowValiadateName.value ||
        isShowValiadateCode.value ||
        isShowValiadateShiftTime1.value ||
        isShowValiadateShiftTime2.value ||
        isShowValiadateShiftTime3.value ||
        isShowValiadateShiftTime4.value ||
        isShowValiadateShiftTime5.value ||
        isShowValiadateShiftTime6.value ||
        isShowValiadateShiftTime7.value ||
        isShowValiadateShiftTime8.value ||
        isShowValiadateShiftTime9.value ||
        isShowValiadateShiftTime10.value
      );
    };
    const onShowPopup = () => {
      if (isCheckValueChange.value === false) {
        isShowPopup.value = !isShowPopup.value;
      } else {
        router.push({ path: "/" });
      }
    };
    const onHideForm = () => {
      router.push({ path: "/" });
    };
    return {
      isShowStartTime,
      isShowEndTime,
      isShowFreeTime,
      isShowBreakStartTimeBetweenShift,
      isShowNoStartTime,
      isShowNoEndTime,
      workingShifts,
      ShiftStartTime,
      TimekeepingShiftStart,
      TimekeepingShiftEnd,
      ShiftEndTime,
      TimekeepingOutShiftStart,
      TimekeepingOutShiftEnd,
      BreakTimeBetweenShiftStart,
      TimekeepingBreakTimeBetweenShifts,
      BreakTimeBetweenShiftEnd,
      BreakTimeBetweenShift,
      WorkingHour,
      handleSubmitForm,
      getData,
      convertTime,
      hour,
      minute,
      second,
      DataName,
      NameDate,
      convertDouble,
      handleWorkingHouse,
      convertToDecimalHours,
      onFormSubmit,
      validateData,
      //#region Validate
      isShowValiadateName,
      isShowValiadateCode,
      isShowValiadateShiftTime1,
      isShowValiadateShiftTime2,
      isShowValiadateShiftTime3,
      isShowValiadateShiftTime4,
      isShowValiadateShiftTime5,
      isShowValiadateShiftTime6,
      isShowValiadateShiftTime7,
      isShowValiadateShiftTime8,
      isShowValiadateShiftTime9,
      isShowValiadateShiftTime10,
      //#endregion
      validateWorkingShiftName,
      validateWorkingShiftCode,
      workingShiftsDefaul,
      isCheckValueChange,
      isShowPopup,
      onShowPopup,
      onHideForm,
      titleForm,
    };
  },
};
</script>
<template>
  <div class="content__top">
    <div class="content__top-left">
      <div class="content__top-icon" @click="onShowPopup">
        <div class="icon"></div>
      </div>
      <div class="content__top-text">{{ titleForm }}</div>
    </div>
    <div class="content-top-right">
      <DxButton
        text="Hủy"
        class="btn_white"
        style="
          width: 100px !important;
          padding: 0px 16px 0px 12px !important;
          border-radius: 4px !important;
          margin-right: 8px;
        "
        @click="onShowPopup"
      ></DxButton>
      <DxButton
        text="Lưu"
        class="btn"
        @click="handleSubmitForm()"
        style="
          width: 100px !important;
          padding: 0px 16px 0px 12px !important;
          border-radius: 4px !important;
        "
      ></DxButton>
    </div>
  </div>

  <DxScrollView style="height: calc(100% - 52px) !important">
    <div class="content__center" style="display: flex; background-color: #fff">
      <div class="content__center-left">
        <form action="your-action" @submit="onFormSubmit($event)">
          <div class="content__row">THÔNG TIN CHUNG</div>
          <!-- Tên ca -->
          <div class="content__row">
            <div class="content__row-text">
              Tên ca<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <DxTextBox
                v-model="workingShifts.WorkingShiftName"
                value-change-event="keyup"
                style="width: 100%"
                :style="{
                  border: isShowValiadateName
                    ? '1px solid #ff6161'
                    : '1px solid #ddd',
                }"
                @value-changed="validateWorkingShiftName($event)"
              >
              </DxTextBox>
              <div class="tooltip" v-if="isShowValiadateName">
                <div class="error-message">
                  <div class="error-icon"></div>
                </div>
                <span class="tooltiptext">Tên ca không được để trống</span>
              </div>
            </div>
          </div>
          <!-- Mã ca -->
          <div class="content__row">
            <div class="content__row-text">
              Mã ca<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <DxTextBox
                v-model="workingShifts.WorkingShiftCode"
                value-change-event="keyup"
                style="width: 100%"
                :style="{
                  border: isShowValiadateCode
                    ? '1px solid #ff6161'
                    : '1px solid #ddd',
                }"
                @value-changed="validateWorkingShiftCode($event)"
              >
              </DxTextBox>
              <div class="tooltip" v-if="isShowValiadateCode">
                <div class="error-message">
                  <div class="error-icon"></div>
                </div>
                <span class="tooltiptext">Mã ca không được để trống</span>
              </div>
            </div>
          </div>
          <!-- Giờ bắt đầu ca -->
          <div class="content__row">
            <div class="content__row-text">
              Giờ bắt đầu ca<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <div class="row-content-col-4">
                <div class="col-5">
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 75%"
                    :style="{
                      border: isShowValiadateShiftTime1
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    dateSerializationFormat="HH:mm:ss"
                    v-model:value="ShiftStartTime"
                    @value-changed="convertTime($event, 'ShiftStartTime')"
                    value-change-event="keyup"
                  >
                  </DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime1">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Bắt đầu không được để trống</span>
                  </div>
                </div>
                <div class="col-5">
                  <DxCheckBox v-model="isShowStartTime" />
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                    >Chấm vào</span
                  >
                </div>
              </div>
              <div class="row-content-col-6" v-if="isShowStartTime">
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 24px;
                    "
                    >Từ</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime2
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="TimekeepingShiftStart"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="convertTime($event, 'TimekeepingShiftStart')"
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime2">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Từ không được để trống</span>
                  </div>
                </div>
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 28px;
                    "
                    >Đến</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime3
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="TimekeepingShiftEnd"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="convertTime($event, 'TimekeepingShiftEnd')"
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime3">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Đến không được để trống</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- Giờ kết thúc ca -->
          <div class="content__row">
            <div class="content__row-text">
              Giờ kết thúc ca<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <div class="row-content-col-4">
                <div class="col-5">
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 75%"
                    :style="{
                      border: isShowValiadateShiftTime4
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="ShiftEndTime"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="convertTime($event, 'ShiftEndTime')"
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime4">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext"
                      >Kết thúc không được để trống</span
                    >
                  </div>
                </div>
                <div class="col-5">
                  <DxCheckBox v-model:value="isShowEndTime"></DxCheckBox>
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                    >Chấm ra</span
                  >
                </div>
              </div>
              <div class="row-content-col-6" v-if="isShowEndTime">
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 24px;
                    "
                    >Từ</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime5
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="TimekeepingOutShiftStart"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="
                      convertTime($event, 'TimekeepingOutShiftStart')
                    "
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime5">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Từ không được để trống</span>
                  </div>
                </div>
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 28px;
                    "
                    >Đến</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime6
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="TimekeepingOutShiftEnd"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="
                      convertTime($event, 'TimekeepingOutShiftEnd')
                    "
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime6">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Đến không được để trống</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- Có nghỉ giữa ca -->
          <div class="content__row">
            <DxCheckBox
              v-model:value="isShowFreeTime"
              style="margin-left: 8px"
            ></DxCheckBox>
            <span
              style="
                font-size: 14px;
                font-family: Roboto, Helvetica, sans-serif;
                color: #212121;
                margin-left: 8px;
              "
              >Có nghỉ giữa ca</span
            >
          </div>
          <!-- Giờ bắt đầu -->
          <div class="content__row" v-if="isShowFreeTime">
            <div class="content__row-text">
              Giờ bắt đầu<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <div class="row-content-col-4">
                <div class="col-5">
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 75%"
                    :style="{
                      border: isShowValiadateShiftTime7
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="BreakTimeBetweenShiftStart"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="
                      convertTime($event, 'BreakTimeBetweenShiftStart')
                    "
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime7">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Bắt đầu không được để trống</span>
                  </div>
                </div>
                <div class="col-5">
                  <DxCheckBox
                    v-model:value="isShowBreakStartTimeBetweenShift"
                  ></DxCheckBox>
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                    >Chấm ra</span
                  >
                </div>
              </div>
              <div
                class="row-content-col-6"
                v-if="isShowBreakStartTimeBetweenShift"
              >
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 16px;
                    "
                    >Đến</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime8
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="TimekeepingBreakTimeBetweenShifts"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="
                      convertTime($event, 'TimekeepingBreakTimeBetweenShifts')
                    "
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime8">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Đến không được để trống</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- Giờ kết thúc ca -->
          <div class="content__row" v-if="isShowFreeTime">
            <div class="content__row-text">
              Giờ kết thúc<span style="color: red">*</span>
            </div>
            <div class="content__row-input">
              <div class="row-content-col-4">
                <div class="col-5">
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 75%"
                    :style="{
                      border: isShowValiadateShiftTime9
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="BreakTimeBetweenShiftEnd"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="
                      convertTime($event, 'BreakTimeBetweenShiftEnd')
                    "
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime9">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext"
                      >Kết thúc không được để trống</span
                    >
                  </div>
                </div>
                <div class="col-5">
                  <DxCheckBox
                    v-model:value="isShowBreakStartTimeBetweenShift"
                  ></DxCheckBox>
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                    >Chấm vào</span
                  >
                </div>
              </div>
              <div
                class="row-content-col-6"
                v-if="isShowBreakStartTimeBetweenShift"
              >
                <div class="col-5">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-right: 16px;
                    "
                    >Đến</span
                  >
                  <DxDateBox
                    type="time"
                    display-format="HH:mm"
                    style="width: 60%"
                    :style="{
                      border: isShowValiadateShiftTime10
                        ? '1px solid #ff6161'
                        : '1px solid #ddd',
                    }"
                    v-model="BreakTimeBetweenShift"
                    dateSerializationFormat="HH:mm:ss"
                    @valueChanged="convertTime($event, 'BreakTimeBetweenShift')"
                  ></DxDateBox>
                  <div class="tooltip" v-if="isShowValiadateShiftTime10">
                    <div class="error-message">
                      <div class="error-icon"></div>
                    </div>
                    <span class="tooltiptext">Đến không được để trống</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- Vẫn tính đủ công khi nhân viên chấm công thiếu giờ vào - ra giữa ca -->
          <div class="content__row" v-if="isShowFreeTime">
            <DxCheckBox
              :disabled="!isShowBreakStartTimeBetweenShift"
              style="margin-left: 8px"
            ></DxCheckBox>
            <span
              style="
                font-size: 14px;
                font-family: Roboto, Helvetica, sans-serif;
                color: #212121;
                margin-left: 8px;
              "
            >
              Vẫn tính đủ công khi nhân viên chấm công thiếu giờ vào - ra giữa
              ca
            </span>
          </div>
          <!-- Tính Công -->
          <div class="content__row">
            <span
              style="
                font-size: 16px;
                font-family: Roboto, Helvetica, Arial, sans-serif;
              "
              >TÍNH CÔNG</span
            >
          </div>
          <!-- Giờ công và Ngày công -->
          <div class="content__row">
            <div class="content__row-text">Giờ công</div>
            <div class="content__row-input">
              <div class="row-content-col-7">
                <div class="col-2">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    :show-spin-buttons="true"
                    format="#0.0"
                    style="width: 75%"
                    v-model:value="WorkingHour"
                    @valueChanged="convertDouble($event, 'WorkingHour')"
                  >
                  </DxNumberBox>
                </div>
                <div class="col-2">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                  >
                    Ngày công
                  </span>
                </div>
                <div class="col-2" style="width: 40%">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 55%; margin-left: 40px"
                    v-model="workingShifts.WorkingDay"
                  >
                  </DxNumberBox>
                </div>
              </div>
            </div>
          </div>
          <!-- Hệ số ngày thường và Hệ số ngày nghỉ -->
          <div class="content__row">
            <div class="content__row-text">Hệ số ngày thường</div>
            <div class="content__row-input">
              <div class="row-content-col-7">
                <div class="col-2">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 75%"
                    v-model="workingShifts.WeekdaySalaryCoefficients"
                  >
                  </DxNumberBox>
                </div>
                <div class="col-2">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                  >
                    Hệ số ngày nghỉ
                  </span>
                </div>
                <div class="col-2" style="width: 40%">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 55%; margin-left: 40px"
                    v-model="workingShifts.WeekendSalaryCoefficient"
                  >
                  </DxNumberBox>
                </div>
              </div>
            </div>
          </div>
          <!-- Hệ số ngày lễ -->
          <div class="content__row">
            <div class="content__row-text">Hệ số ngày lễ</div>
            <div class="content__row-input">
              <div class="row-content-col-7">
                <div class="col-2">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 75%"
                    v-model="workingShifts.HolidayPayCoefficient"
                  >
                  </DxNumberBox>
                </div>
              </div>
            </div>
          </div>
          <!-- Nếu không có giờ vào thì bị trừ công -->
          <div class="content__row">
            <DxCheckBox
              v-model:value="isShowNoStartTime"
              style="margin-left: 8px"
            ></DxCheckBox>
            <span
              style="
                font-size: 14px;
                font-family: Roboto, Helvetica, sans-serif;
                color: #212121;
                margin-left: 8px;
              "
            >
              Nếu không có giờ vào thì bị trừ công
            </span>
          </div>
          <!-- Giờ công và Ngày Công -->
          <div class="content__row" v-if="isShowNoStartTime">
            <div class="content__row-text">Giờ công</div>
            <div class="content__row-input">
              <div class="row-content-col-7">
                <div class="col-2">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    :show-spin-buttons="true"
                    style="width: 75%"
                  >
                  </DxNumberBox>
                </div>
                <div class="col-2">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                  >
                    Ngày công
                  </span>
                </div>
                <div class="col-2" style="width: 40%">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    :show-spin-buttons="true"
                    style="width: 55%; margin-left: 40px"
                  >
                  </DxNumberBox>
                </div>
              </div>
            </div>
          </div>
          <!-- Nếu không có giờ ra thì bị trừ công -->
          <div class="content__row">
            <DxCheckBox
              v-model:value="isShowNoEndTime"
              style="margin-left: 8px"
            ></DxCheckBox>
            <span
              style="
                font-size: 14px;
                font-family: Roboto, Helvetica, sans-serif;
                color: #212121;
                margin-left: 8px;
              "
            >
              Nếu không có giờ ra thì bị trừ công
            </span>
          </div>
          <!-- Giờ công và Ngày Công -->
          <div class="content__row" v-if="isShowNoEndTime">
            <div class="content__row-text">Giờ công</div>
            <div class="content__row-input">
              <div class="row-content-col-7">
                <div class="col-2">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 75%"
                  >
                  </DxNumberBox>
                </div>
                <div class="col-2">
                  <span
                    style="
                      font-size: 14px;
                      font-family: Roboto, Helvetica, sans-serif;
                      color: #212121;
                      margin-left: 8px;
                    "
                  >
                    Ngày công
                  </span>
                </div>
                <div class="col-2" style="width: 40%">
                  <DxNumberBox
                    :min="0"
                    :max="24"
                    format="#0.0"
                    :show-spin-buttons="true"
                    style="width: 55%; margin-left: 40px"
                  >
                  </DxNumberBox>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
      <div class="content__center-right">
        <div class="content__row">TÙY CHỈNH NÂNG CAO</div>
        <div class="content__row">
          <div class="content__row-check">
            <DxCheckBox></DxCheckBox>
          </div>
          <div class="content__row-text">Đi muộn về sớm</div>
        </div>
        <div class="content__row">
          <div class="content__row-check">
            <DxCheckBox></DxCheckBox>
          </div>
          <div class="content__row-text">Làm thêm giờ</div>
        </div>
        <div class="content__row">
          <div class="content__row-check">
            <DxCheckBox></DxCheckBox>
          </div>
          <div class="content__row-text">Công ăn ca</div>
        </div>
        <div class="content__row">
          <div class="content__row-check">
            <DxCheckBox></DxCheckBox>
          </div>
          <div class="content__row-text">Công điều động</div>
        </div>
      </div>
    </div>
  </DxScrollView>
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
        <div class="popupHeader-right" @click="onShowPopup">
          <div class="popupHeader-icon"></div>
        </div>
      </div>
      <div class="popupContent">
        <span class="popupText"
          >Thông tin đã được thay đổi. Bạn có muốn lưu lại không?</span
        >
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
          @click="onShowPopup"
        ></DxButton>
        <DxButton
          class="btn_Cancel btn_white"
          text="Không lưu"
          style="
            border-radius: 4px !important;
            padding: 6px 5px !important;
            width: 80px !important;
            margin-right: 16px;
          "
          @click="onHideForm"
        ></DxButton>
        <DxButton
          class="btn"
          text="Lưu"
          style="
            border-radius: 4px !important;
            padding: 6px 5px !important;
            width: 80px !important;
          "
          @click="handleSubmitForm"
        ></DxButton>
      </div>
    </div>
  </DxPopup>
</template>
<style scoped>
.content__top {
  align-items: center;
}
@import url(../css/components/dxcheckbox.css);
</style>