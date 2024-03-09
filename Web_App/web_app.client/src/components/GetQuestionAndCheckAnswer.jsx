import React, { useState } from "react";

const GetQuestionAndCheckAnswer = ({ name, numQuestions }) => {
    const buttonEnabledClassName = "instruction-button";
    const buttonDisabledClassName = "instruction-button-disabled";

    const [initButtonDisabled, setInitButtonDisabled] = useState(false);
    const [getQuestionButtonDisabled, setGetQuestionButtonDisabled] = useState({
        disabled: false,
        className: buttonEnabledClassName,
    });
    const [submitAnswerButtonDisabled, setSubmitAnswerButtonDisabled] = useState({
        disabled: true,
        className: buttonDisabledClassName,
    });
    const [displayAnswer, setDisplayAnswer] = useState(false);
    const [question, setQuestion] = useState(null);
    const [writtenAnswer, setWrittenAnswer] = useState('');

    const handleInitializeQuiz = async () => {
        try {
            await fetch(`https://localhost:7140/api/quiz/initquiz/${name}/${numQuestions}`);
            setInitButtonDisabled(true);
          } catch (error) {
            console.error('Initialization error:', error);
          }
    };

    const handleGetQuestion = async () => {
        setGetQuestionButtonDisabled({
            disabled: true,
            className: buttonDisabledClassName,
        });
        const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}`);
        const retrievedQuestion = await response.json();
        setQuestion(retrievedQuestion);
        setDisplayAnswer(true);
        if (retrievedQuestion.questionType === 'MCSACard') {
            setSubmitAnswerButtonDisabled({
                disabled: false,
                className: buttonEnabledClassName,
            });
        };
    };

    const handleSubmitAnswer = async (answer) => {
        setSubmitAnswerButtonDisabled({
            disabled: true,
            className: buttonDisabledClassName,
        });
        // Answer is in correct format for both QuestionCard and MCSACard.
        
    };

    const handleInputChange = (e) => {
        const inputValue = e.target.value;
        setWrittenAnswer(inputValue);
        setSubmitAnswerButtonDisabled({
            disabled: inputValue.trim().length === 0,
            className: inputValue.trim().length === 0 ? buttonDisabledClassName : buttonEnabledClassName,
        });
    };

    return (
        <>

        {!initButtonDisabled ? (
            <button 
            className={buttonEnabledClassName}
            onClick={handleInitializeQuiz}
            disabled={initButtonDisabled}>
                Initialize Quiz
            </button>
        )
        : (
             <button
             className={getQuestionButtonDisabled.className}
             onClick={handleGetQuestion}
             disabled={getQuestionButtonDisabled.disabled}>
                Get Question
             </button>
            )}

        {displayAnswer && question && (
            <>

                <h3>{question.questionText}</h3>

                {question.questionType === 'QuestionCard' && (
                    <>
                        <label htmlFor="writtenAnswer">Input answer:</label>
                        <input
                            type="text"
                            id="writtenAnswer"
                            value={writtenAnswer}
                            onChange={handleInputChange}
                        />
                        <button
                            className={submitAnswerButtonDisabled.className}
                            disabled={submitAnswerButtonDisabled.disabled}
                            onClick={() => handleSubmitAnswer(writtenAnswer)}
                        >
                            Submit Answer
                        </button>
                    </>
                )}

                {question.questionType === "MCSACard" && (
                    <div>
                        {[question.option1, question.option2, question.option3, question.option4, question.option5].map((option, index) => (
                            <button
                                key={index}
                                className={submitAnswerButtonDisabled.className}
                                disabled={submitAnswerButtonDisabled.disabled}
                                onClick={() => handleSubmitAnswer(index + 1)}
                            >
                                {option}
                            </button>
                        ))}
                    </div>
                )}

            </>
        )}
        
        </>
    );
};

export default GetQuestionAndCheckAnswer;