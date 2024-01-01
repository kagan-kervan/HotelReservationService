// Login.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import './Login.css';
import axios, { formToJSON } from '../../../node_modules/axios/index';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [accountPassword, setaccountPassword] = useState('');
  const [isLoggedIn, setLoggedIn] = useState(false);

  const handleLogin = () => {
    // Burada gerçek bir kimlik doğrulama işlemi gerçekleştirilebilir.
    // // // Ancak bu örnek için sadece kullanıcı adı ve şifre kontrolü yapılıyor.
    axios.get("https://localhost:3000/api/Customer/get-customer?email="+username,{
      timeout: 10000,
    })
    .then(response =>
      response.data
    ).then((data) => {
      console.log(data);   // Check if the necessary properties exist before accessing them
      console.log(data.password)
        setaccountPassword(data.password);
        // If account password is working.
        if (password === data.password) {
          setLoggedIn(true);
        } else {
          alert('Kullanıcı adı veya şifre hatalı!');
        }
      }
    ).catch(error => {
      console.error('Error fetching customer data:', error);
      // Handle error as needed
    });
    //POST EXAMPLE
  //   axios.post("https://localhost:3000/api/Customer/add-customer",
  //   {
  // "name": "ReactTest",
  // "surname": "Reacto",
  // "email_Address": "reacting.com",
  // "password": "reactg",
  // "phone": "5554444222"
  //   }).then(response => {console.log(response.data)}).catch(error => {console.error("error postin",error)});
  
}

  const handleLogout = () => {
    setLoggedIn(false);
    setUsername('');
    setPassword('');
  };

  return (
    <div className='main'>
      {isLoggedIn ? (  
           window.location.href = '/'
      ) : (
        <div className='login-container'>
          <h2>Giriş Yap</h2>
          <label>
            Kullanıcı Adı:
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
          </label>
          <br />
          <label>
            Şifre:
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </label>
          <br />
          <button onClick={handleLogin}>Giriş Yap</button>
          <p>
            Hesabınız yok mu?{' '}

            {/* REGİSTER LİNKİ EKLE */}
            {/* <Link to="/Register">Kayıt Ol</Link> */}
            <Link to="/Register"> <button type="button" className="btn btn-md btn-outline-danger">Kayıt Ol</button></Link>
          </p>
        </div>
      )}
    </div>
  );
};
 
export default Login;
