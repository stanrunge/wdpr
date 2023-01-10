import Header from './Header';
import Footer from './Footer';

export default function (props) {
  return (
    <>
        <Header />
          <main>
              {props.children}
          </main>
        <Footer />
    </>
  )
}