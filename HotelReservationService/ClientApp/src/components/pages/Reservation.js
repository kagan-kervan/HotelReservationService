import React, { Component } from 'react';
import './reservation.css';

class Reservation extends Component {
    constructor(props) {
        super(props);

        this.state = {
            firstName: '',
            lastName: '',
            phone_no: '',
            email: '',
            cvc: '',
            creditCardNumber: '',
        };
    }

    handleChange = (e) => {
        this.setState({
            [e.target.name]: e.target.value,
        });
    };

    handleSubmit = (e) => {
        e.preventDefault();
        // Burada form verileri ile ne yapmak istediğinizi ekleyebilirsiniz
        console.log('Form Verileri:', this.state);
        // Örneğin, rezervasyonu tamamla işlemleri burada gerçekleşebilir
    };

    render() {
        return (
            <div className='general'>
                <h1>ÖDEME SAYFASI</h1>
                <form onSubmit={this.handleSubmit}>
                    <div className='kullanici_bilgileri'>
                        <label>
                            Ad:
                            <input
                                type="text"
                                name="firstName"
                                value={this.state.firstName}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                        <label>
                            Soyad:
                            <input
                                type="text"
                                name="lastName"
                                value={this.state.lastName}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                        <label>
                            Telefon no:
                            <input
                                type="text"
                                name="phone_no"
                                value={this.state.phone_no}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                        <label>
                            Email:
                            <input
                                type="email"
                                name="email"
                                value={this.state.email}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                    </div>

                    <div className='ödeme_bilgileri'>
                        <label>
                            Kredi Kartı Numarası:
                            <input
                                type="text"
                                name="creditCardNumber"
                                value={this.state.creditCardNumber}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                        <label>
                            CVC:
                            <input
                                type="text"
                                name="cvc"
                                value={this.state.cvc}
                                onChange={this.handleChange}
                                required
                            />
                        </label>
                        <br />
                    </div>


                    <button type="submit">Rezervasyonu Tamamla</button>
                </form>
            </div>
        );
    }
}

export default Reservation;
