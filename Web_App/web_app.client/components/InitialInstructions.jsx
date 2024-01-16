import React from "react";
// TODO fix this component after remaking API/InterfaceHandler
const InitialInstructions = () => 
{
    const apiUrl = 'https://localhost:7140/api/quiz/instructions';

    let instructions;

    fetch(apiUrl)
    .then(response => {
        if (!response.ok) {
        throw new Error(`HTTP error. Status: ${response.status}`);
        }
        instructions = response.json();
    })
    .then(data => {
        instructions = ('Response data:', data);
    })
    .catch(error => {
        instructions = ('Fetch error:', error);
    });

    return (
        <>
        <h3>Temporary instructions</h3>
        </>
    );
};
//Get initial instructions through API.
export default InitialInstructions;