import { Link } from "react-router-dom";
 
function Home() {
  return (
    <div>
 
      {/* NAVBAR */}
      <nav className="navbar navbar-expand-lg navbar-dark px-4 border-bottom">
        <div className="container">
          <Link className="navbar-brand fw-bold text-emerald" to="/">
            Therapy Center
          </Link>
 
          <button
            className="navbar-toggler"
            data-bs-toggle="collapse"
            data-bs-target="#nav"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
 
          <div className="collapse navbar-collapse" id="nav">
            <ul className="navbar-nav ms-auto align-items-center">
              <li className="nav-item">
                <Link className="nav-link" to="/">Home</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/services">Services</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/about">About</Link>
              </li>
              <li className="nav-item ms-3">
                <Link className="btn btn-emerald" to="/login">
                  Login
                </Link>
                <Link className="btn btn-emerald " to="/register">
                  Register
                </Link>
              </li>
            </ul>
          </div>
        </div>
      </nav>
 
      {/* HERO */}
      <section className="section d-flex align-items-center text-center border-bottom" style={{ minHeight: "90vh" }}>
        <div className="container">
          <h1 className="fw-bold">
            Empowering Special Kids <br />
            <span className="text-emerald">
              Through Therapy & Care
            </span>
          </h1>
 
          <p className="text-mute mt-3">
            Book sessions, track progress, and connect with expert therapists —
            all in one platform.
          </p>
 
          <div className="mt-4">
            <Link to="/register" className="btn btn-emerald me-3">
              Get Started
            </Link>
            <Link to="/login" className="btn btn-outline-light">
              Login
            </Link>
          </div>
        </div>
      </section>
 
      {/* FEATURES */}
      <section className="section">
        <div className="container text-center">
          <h2 className="mb-5">Why Choose Us</h2>
 
          <div className="row">
            {[
              { title: "Expert Therapists", desc: "Certified professionals for specialized care." },
              { title: "Easy Booking", desc: "Seamless appointment scheduling anytime." },
              { title: "Progress Tracking", desc: "Monitor therapy reports & improvements." }
            ].map((item, i) => (
              <div key={i} className="col-md-4 mb-4">
                <div className="card-dark p-4 h-100">
                  <h5 className="text-emerald">{item.title}</h5>
                  <p className="text-mute">{item.desc}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
 
      {/* SERVICES */}
      <section className="section">
        <div className="container text-center">
          <h2 className="mb-4">Our Therapies</h2>
 
          <div className="row">
            {["Speech", "Occupational", "Behavioral", "Physical"].map((s, i) => (
              <div key={i} className="col-md-3 col-6 mb-4">
                <div className="border-emerald p-3 rounded">
                  {s} Therapy
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
 
      {/* CTA */}
      <section className="section text-center border-top">
        <h3>Start Your Journey Today</h3>
        <Link to="/register" className="btn btn-emerald mt-3">
          Register Now
        </Link>
      </section>
 
      {/* FOOTER */}
      <footer className="text-center py-3 border-top">
        <p className="text-mute mb-0">© 2026 Therapy Center</p>
      </footer>
 
    </div>
  );
}
 
export default Home;
 