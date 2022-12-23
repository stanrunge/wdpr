import {useEffect, useState} from "react";

function Uitvoering() {
    const [uitvoeringen, setUitvoeringen] = useState([]);

    useEffect(() => {
        async function getUitvoering(id) {
            const response = await fetch(`http://localhost:16990/Uitvoering`);
            const data = await response.json();
            setUitvoeringen(data);
        }

        getUitvoering(2);
    }, []);

    const items = uitvoeringen.map(item => (
      <li key={item.id}>Begin tijd: {item["beginTijd"]}, Eind tijd: {item["eindTijd"]}</li>
    ));

    return (
        <>
            <h2>Uitvoeringen:</h2>
            <ul>
                {items}
            </ul>
        </>
    );
}

export default Uitvoering;