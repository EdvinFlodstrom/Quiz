import React, { useState, useEffect } from "react"; // TODO : Fix the question string not being rendered.

const GetQuestionAndCheckAnswer = ({ name, numQuestions }) => {
    const [question, setQuestion] = useState(null);
    const [answer, setAnswer] = useState("");
    const [submittedAnswer, setSubmittedAnswer] = useState("");
    const [showAnswer, setShowAnswer] = useState(false);

    useEffect(() => {
        // Function to initialize quiz and fetch the first question from the API
        const initQuizAndFetchQuestion = async () => {
            try {
                // Initialize quiz (you may modify the endpoint and handling based on your server)
                await fetch(`https://localhost:7140/api/quiz/initquiz/${name}/${numQuestions}`);

                // Fetch the first question
                const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}`);
                if (!response.ok) {
                    throw new Error(`HTTP error. Status: ${response.status}`);
                }

                const data = await response.json();
                setQuestion(data);
                setAnswer(""); // Clear previous answer
                setShowAnswer(false);
            } catch (error) {
                console.error('Fetch error:', error);
            }
        };

        initQuizAndFetchQuestion(); // Fetch question when the component mounts or when the user gets the next question
    }, [name, submittedAnswer]);

    const handleAnswerChange = (e) => {
        setAnswer(e.target.value);
    };

    const handleOptionClick = (option) => {
        setAnswer(option);
    };

    const handleSubmitAnswer = async () => {
        try {
            const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}/${answer}`);
            if (!response.ok) {
                throw new Error(`HTTP error. Status: ${response.status}`);
            }

            const data = await response.json();
            setSubmittedAnswer(data);
            setShowAnswer(true);
        } catch (error) {
            console.error('Submit answer error:', error);
        }
    };

    const handleGetNextQuestion = () => {
        // This will trigger a re-fetch of the question by updating the submittedAnswer state
        setSubmittedAnswer("");
    };

    return (
        <div>
            <h3>{question ? question.QuestionText : "Loading question..."}</h3>

            {question && question.QuestionType === "MCSACard" ? (
                <>
                    <div>
                        <button onClick={() => handleOptionClick(question.Option1)}>{question.Option1}</button>
                        <button onClick={() => handleOptionClick(question.Option2)}>{question.Option2}</button>
                        <button onClick={() => handleOptionClick(question.Option3)}>{question.Option3}</button>
                        <button onClick={() => handleOptionClick(question.Option4)}>{question.Option4}</button>
                        <button onClick={() => handleOptionClick(question.Option5)}>{question.Option5}</button>
                    </div>
                </>
            ) : (
                <>
                    <div>
                        <label>
                            Your Answer:
                            <input type="text" value={answer} onChange={handleAnswerChange} />
                        </label>
                    </div>
                </>
            )}

            <div>
                <button onClick={handleSubmitAnswer} disabled={!answer}>
                    Submit Answer
                </button>
            </div>

            {showAnswer && (
                <div>
                    <p>Your submitted answer: {submittedAnswer}</p>
                    <button onClick={handleGetNextQuestion} disabled={!submittedAnswer}>
                        Get Next Question
                    </button>
                </div>
            )}
        </div>
    );
};

export default GetQuestionAndCheckAnswer;
