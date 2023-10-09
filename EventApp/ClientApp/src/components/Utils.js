
const isLoggedIn = () => {
    const jwt = localStorage.getItem('jwt');
    return jwt !== null && jwt?.length > 0
}
export default isLoggedIn;
