import React, { useEffect, useState } from "react";
import TakeQuiz from "../components/TakeQuiz";
import CreateQuestion from "../components/CreateQuestion";
import RemoveQuestion from "../components/RemoveQuestion";
import ModifyQuestion from "../components/ModifyQuestion";
import CloseApplication from "../components/CloseApplication";
import '../styles/InstructionButtons.css';

const InitialInstructions = () => {
    const apiUrl = 'https://localhost:7140/api/quiz/instructions';
    const [instructions, setInstructions] = useState(null);
    const [selectedComponent, setSelectedComponent] = useState(null);

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
        switch (buttonIndex) {
            case 1:                
                setSelectedComponent(<TakeQuiz />);
                break;
            case 2:
                setSelectedComponent(<CreateQuestion />);
                break;
            case 3:
                setSelectedComponent(<RemoveQuestion />);
                break;
            case 4:
                setSelectedComponent(<ModifyQuestion />);
                break;
            case 5:
                setSelectedComponent(<CloseApplication />);
                break;
            default:
                setSelectedComponent(null);
                break;
        }
    };

    return (
        <>
            {instructions ? (
                <>
                    <h1>Quiz</h1>
                    {selectedComponent ? (
                        <>
                            {selectedComponent}
                        </>
                    ) : (
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
                        </>
                    )}
                </>
            ) : (
                <p>Loading...</p>
            )}
        </>
    );
};

export default InitialInstructions;
