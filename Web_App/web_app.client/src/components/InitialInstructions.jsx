import React, { useEffect, useState } from "react";
import '../styles/InstructionButtons.css';
import TakeQuiz from './TakeQuiz';
import CreateQuestion from './CreateQuestion';
import RemoveQuestion from './RemoveQuestion';
import ModifyQuestion from './ModifyQuestion';
import CloseApplication from './CloseApplication';

const InitialInstructions = () => {
    const apiUrl = 'https://localhost:7140/api/quiz/instructions';
    const [instructions, setInstructions] = useState(null);
    const [selectedButton, setSelectedButton] = useState(null);

    const fetchData = async () => {
        try {
            const response = await fetch(apiUrl);

            if (!response.ok) {
                throw new Error(`HTTP error. Status: ${response.status}`);
            }

            const data = await response.json();
            setInstructions(data);
        } catch (error) {
            console.error('Fetch error:', error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    const handleButtonClick = (buttonIndex) => {
        setSelectedButton(buttonIndex);
    };

    const renderComponent = () => {
        switch (selectedButton) {
            case 1:
                return <TakeQuiz />;
            case 2:
                return <CreateQuestion />;
            case 3:
                return <RemoveQuestion />;
            case 4:
                return <ModifyQuestion />;
            case 5:
                return <CloseApplication />;
            default:
                return null;
        }
    };

    return (
        <>
            {instructions ? (
                <>
                    <h4>{instructions[0]}</h4>
                    <div>
                        {instructions.slice(1).map((instruction, index) => (
                            <React.Fragment key={index}>
                                <button
                                    className="instruction-button"
                                    onClick={() => handleButtonClick(index + 1)}
                                >
                                    {instruction}
                                </button>
                                <br />
                            </React.Fragment>
                        ))}
                    </div>
                    <div>
                        {renderComponent()}
                    </div>
                </>
            ) : (
                <p>Loading...</p>
            )}
        </>
    );
};

export default InitialInstructions;
