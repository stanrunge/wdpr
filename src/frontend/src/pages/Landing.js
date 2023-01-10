import {Link} from "react-router-dom";
import Layout from "../components/Layout";

function Landing() {
    return (
        <Layout>
          <img src={"/images/logo.png"} alt="logo"/>
          <rect>
            <p>Theater Laak Nieuws</p>
          </rect>

          <div className={"kalender-gallery"}>

          </div>
          
          <button>Tickets</button>
          <button>Tickets</button>
          <Link to={"/Kalender"}>
            <button>Volledige programma</button>
          </Link>
          <button>Tickets</button>
          <button>Huur een zaal</button>
          <Link to={"/informatie"}>
            <button>Over Theater Laak</button>
          </Link>
          <button>Steun het theater</button>
        </Layout>
    );
}

export default Landing;