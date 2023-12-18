// Login.js
import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setLoggedIn] = useState(false);

  const handleLogin = () => {
    // Burada gerçek bir kimlik doğrulama işlemi gerçekleştirilebilir.
    // Ancak bu örnek için sadece kullanıcı adı ve şifre kontrolü yapılıyor.
    if (username === 'kullanici' && password === 'sifre') {
      setLoggedIn(true);
    } else {
      alert('Kullanıcı adı veya şifre hatalı!');
    }
  };

  const handleLogout = () => {
    setLoggedIn(false);
    setUsername('');
    setPassword('');
  };

  return (
    <div>
      {isLoggedIn ? (  
        // burası anasayfa olarak degistirilecek
           window.location.href = '/'
      ) : (
        <div>
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
            <Link to="/register">Kayıt Ol</Link>
          </p>
          {isLoggedIn && (
            <p>
              Başarıyla giriş yaptınız! Şimdi <Link to="/navbar">navbar</Link> sayfasına gidin.
            </p>
          )}
        </div>
      )}
    </div>
  );
};

export default Login;
