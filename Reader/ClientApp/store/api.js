import axios from 'axios'

const API_URL = 'api';
const API_CATALOG = API_URL + '/catalog';
const API_BOOK = API_URL + '/book';
const API_CATALOG_GET_USER_CATALOGS = API_CATALOG + '/getcatalogs';
const API_CATALOG_GET_CATALOG_NAMES = API_CATALOG + '/GetCatalogNames';
const API_CATALOG_POST_ADD_BOOK = API_CATALOG + '/AddBookToCatalog';
const API_CATALOG_POST_REMOVE_BOOK = API_CATALOG + '/RemoveBookFromCatalog';
const API_CATALOG_GET_CATALOG_BOOKS = API_CATALOG + '/getcatalog';
const API_CATALOG_GET_CATALOG_UPLOADED = API_CATALOG + '/getuploadedboooks';
const API_CATALOG_POST_CATALOG_NEW = API_CATALOG + '/addcatalog';
const API_BOOK_GET_SEARCH_BOOK = API_BOOK + '/searchbook';


function errorHandler(error) {
    console.log("Error occured"+error);
}

export const getBook = async (id="") => {
    let response = await axios.get("/api/book/" + id);
    return response.data;
}

export const postBook = async (bookFile) => {
    let data = new FormData();
    data.append('uploadedFile', bookFile);

    let response = await axios.post('/api/book/addbook', data);
    return response.data;
}

export const getUserCatalogs = async (userId) => {
    let response = await axios.get(API_CATALOG_GET_USER_CATALOGS + "?userId=" + userId);
    console.log('response');
    console.log(response);
    return response.data;
}

export const getCatalog = async (catalogId) => {
    let response = await axios.get(API_CATALOG_GET_CATALOG_BOOKS + "?catalogId=" + catalogId);
    return response.data;
}

export const getUploadedBooksCatalog = async (userId) => {
    let response = await axios.get(API_CATALOG_GET_CATALOG_UPLOADED + "?userId=" + userId);
    return response.data;
}

export const postNewCatalog = async (catalogName) => {
    let data = {
        'name': catalogName
    };
    let response = await axios.post(API_CATALOG_POST_CATALOG_NEW, data);
    return response.data;
}

export const getSearchBooks = async (bookName="", authorName="") => {
    let response = await axios.get(API_BOOK_GET_SEARCH_BOOK + "?bookName=" + bookName + "&authorName=" + authorName);
    return response.data;
}

export const getCatalogNames = async (userId, bookId) => {
    console.log(userId);
    let response = await axios.get(API_CATALOG_GET_CATALOG_NAMES + "?userId=" + userId + "&bookId="+bookId);
    return response.data;
}

export const postAddBookToCatalog = async (catalogId, bookId) => {
    let data = {
        'catalogId': catalogId,
        'bookId': bookId
    };
    let response = await axios.post(API_CATALOG_POST_ADD_BOOK, data);
    return response.data;
}
export const postRemoveBookFromCatalog = async (catalogId, bookId) => {
    let data = {
        'catalogId': catalogId,
        'bookId': bookId
    };
    let response = await axios.post(API_CATALOG_POST_REMOVE_BOOK, data);
    return response.data;
}
