import axios from 'axios'

function errorHandler(error) {
    console.log("Error occured"+error);
}

export default {
    async getBook(id="") {
        let response = await axios.get("/api/book/"+id);
        console.log(response.data);
        return response.data;
    },

    async getBookPreview(id = "") {
        let response = await axios.get("/api/bookpreview/" + id);
        console.log(response.data);
        return response.data;
    },

    async postBook(bookFile) {
        let data = new FormData();
        data.append('uploadedFile',bookFile);

        let response = await axios.post('/api/book/addbook', data);
        return response.data;
    }
}
