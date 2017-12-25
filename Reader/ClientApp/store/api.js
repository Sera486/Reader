import axios from 'axios'

function errorHandler(error) {
    console.log("Error occured"+error);
}

export default {
    async login(user) {
        let response = await axios.post("/api/account/login",
            {
                userName: user.userName,
                password: user.password,
                remeberMe: user.remeberMe
            });
        localStorage.setItem("token", response.data.token);
        //this.user.authenticated = true;
        return response.data;
    }
}
