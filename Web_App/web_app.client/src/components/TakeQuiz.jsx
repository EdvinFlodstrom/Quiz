import React, { useState } from "react";
import GetQuestionAndCheckAnswer from './GetQuestionAndCheckAnswer';

const TakeQuiz = () => {
    const [name, setName] = useState("");
    const [numQuestions, setNumQuestions] = useState("");
    const [showQuestions, setShowQuestions] = useState(false);

    const handleSubmit = () => {
        // You can perform additional validation or processing here
        setShowQuestions(true);
    };

    return (
        <div>
            <h2>Welcome to the quiz! To get your first question, please press the button below.</h2>
            
            <div>
                <label htmlFor="name">Enter name:</label>
                <input
                    type="text"
                    id="name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>

            <div>
                <label htmlFor="numQuestions">Enter your amount of questions:</label>
                <input
                    type="text"
                    id="numQuestions"
                    value={numQuestions}
                    onChange={(e) => setNumQuestions(e.target.value)}
                />
            </div>

            <button className="instruction-button" onClick={handleSubmit}>
                Submit name and number of questions
            </button>

            {showQuestions && (
                <GetQuestionAndCheckAnswer
                    name={name}
                    numQuestions={numQuestions}
                />
            )}
        </div>
    );
};

export default TakeQuiz;
