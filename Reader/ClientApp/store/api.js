import axios from 'axios'

function errorHandler(error) {
    console.log("Error occured"+error);
}

export const getBook = async (id="") => {
    let response = await axios.get("/api/book/" + id);
    console.log(response.data);
    return response.data;
}

export const postBook = async (bookFile) => {
    let data = new FormData();
    data.append('uploadedFile', bookFile);

    let response = await axios.post('/api/book/addbook', data);
    return response.data;
}

