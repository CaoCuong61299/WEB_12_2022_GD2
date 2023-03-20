import axios from "axios";

const API = "https://localhost:44325/api"

const APIWorkingShift = {
    async GetAll(){
        return await axios.get(`${API}/WorkingShift`);
    },
    async GetByPagingAndFilter(custom,params){
        return await axios.post(`${API}/WorkingShift/pagingAndFilter?`,custom,{params})
    },
    async PostWorkingShift (WorkingShift){
        return await axios.post(`${API}/WorkingShift`,WorkingShift)
    },
    async PutWorkingShift(WorkingShiftId,WorkingShift){
        return await axios.put(`${API}/WorkingShift/${WorkingShiftId}`,WorkingShift)
    },
    async GetByWorkingShiftId(id){
        return await axios.get(`${API}/WorkingShift/${id}`)
    },
    async deleteManyWorkingShift(params){
        return await axios.delete(`${API}/WorkingShift/recordIds`,{data:params,})
    },
    async deleteOneWorkingShift(params){
        return await axios.delete(`${API}/WorkingShift/${params}`);
    },
    async exportExcel(){
        return await axios.get(`${API}/WorkingShift/export`,{responseType: 'blob'})
    },
}

export default APIWorkingShift;