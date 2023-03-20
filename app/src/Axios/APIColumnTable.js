import axios from "axios";

const API = "https://localhost:44325/api/TableColumn"

export const APIColumnTable = {
    async GetAll(){
        return await axios.get(`${API}`)
    },
    async PostColumn(params){
        return await axios.post(`${API}`,params)
    },
    async DeleteColumn(){
        return await axios.delete(`${API}`)
    }
}