import React, { useState } from 'react';
import { registerUser } from '../../services/authService';
import { useNavigate, Link } from 'react-router-dom';
 
const RegisterForm = () => {
  const navigate = useNavigate();
 
  const [formData, setFormData] = useState({
    firstName:"",
    lastName:"",
    email:"",
    password:"",
    phoneNumber:"",
    role:"Patient",
  });
 
  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };
 
  const handleSubmit = async (e) => {
    e.preventDefault();
 
    try {
      await registerUser(formData);
      alert("Registered Successfully");
      navigate("/login");
    } catch (error) {
      console.error(error);
    }
  };
 
  return (
    <div className="container-fluid">
      <div className="row min-vh-100">
 
        {/* LEFT SIDE */}
        <div className="col-lg-6 d-none d-lg-flex align-items-center justify-content-center auth-left">
          <div className="text-center px-4">
            <h1 className="fw-bold text-emerald">Therapy Center</h1>
            <p className="mt-3 text-mute">
              Helping children grow with care, therapy, and love
            </p>
          </div>
        </div>
 
        {/* RIGHT SIDE */}
        <div className="col-lg-6 col-12 d-flex align-items-center justify-content-center auth-right">
          <div className="auth-card p-4">
 
            <h3 className="text-center mb-3">Create Account</h3>
 
            <form onSubmit={handleSubmit}>
              <div className="row">
                <div className="col-md-6 mb-3">
                  <input
                    type="text"
                    name="firstName"
                    className="form-control"
                    placeholder="First Name"
                    onChange={handleChange}
                    required
                  />
                </div>
 
                <div className="col-md-6 mb-3">
                  <input
                    type="text"
                    name="lastName"
                    className="form-control"
                    placeholder="Last Name"
                    onChange={handleChange}
                    required
                  />
                </div>
              </div>
 
              <div className="mb-3">
                <input
                  type="email"
                  name="email"
                  className="form-control"
                  placeholder="Email Address"
                  onChange={handleChange}
                  required
                />
              </div>
 
              <div className="mb-3">
                <input
                  type="password"
                  name="password"
                  className="form-control"
                  placeholder="Password"
                  onChange={handleChange}
                  required
                />
              </div>
 
              <div className="mb-3">
                <input
                  type="text"
                  name="phoneNumber"
                  className="form-control"
                  placeholder="Phone Number"
                  onChange={handleChange}
                  required
                />
              </div>
 
              <div className="mb-3">
                <select
                  name="role"
                  className="form-select"
                  onChange={handleChange}
                >
                  <option value="Patient">Patient</option>
                  <option value="Guardian">Guardian</option>
                </select>
              </div>
 
              <button className="btn btn-emerald w-100 py-2">
                Register
              </button>
 
              <p className="text-center mt-3 text-mute">
                Already have an account?{" "}
                <Link to="/login" className="auth-link">
                  Login
                </Link>
              </p>
 
            </form>
          </div>
        </div>
 
      </div>
    </div>
  );
};
 
export default RegisterForm;
 