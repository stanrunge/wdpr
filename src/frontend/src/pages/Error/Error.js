import { useRouteError } from "react-router-dom";

function Error() {
  const error = useRouteError();
  console.log(error);
  
  return (
    <div>
      <h1>{error.status}: {error.statusText}</h1>
    </div>
  );
}

export default Error;