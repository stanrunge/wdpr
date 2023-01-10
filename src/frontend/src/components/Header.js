import {Link} from "react-router-dom";

export default function Header() {
  return (
    <header>
      <h1>Theater Laak</h1>
      <Link to={"/"}>Home</Link>
      <Link to={"/kalender"}>Kalender</Link>
      <Link to={"/account"}>Account</Link>
    </header>
  );
}