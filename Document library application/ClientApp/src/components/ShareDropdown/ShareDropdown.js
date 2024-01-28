import React, { useState, useEffect } from 'react';
import { ButtonGroup, Button, Dropdown, Modal } from 'react-bootstrap';
import './ShareDropdown.css';

function ShareDropdown({ attachmentId, handleShare }) {
    const [showModal, setShowModal] = useState(false);
    const [generatedLink, setGeneratedLink] = useState('');
    const [selectedOption, setSelectedOption] = useState('');
    const [publicURL, setPublicURL] = useState('');

    useEffect(() => {
        setPublicURL(window.location.protocol + window.location.host);
 
    });

    const handleDropdownSelect = async (eventKey, event) => {
        setSelectedOption(eventKey);
        await generateLink(eventKey);
    };

    const generateLink = async (option) => {
        try {
            const url = `attachment/link/${attachmentId}/${option}`;
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            const responseData = await response.json();

            const message = responseData.Message;

            setGeneratedLink(publicURL + '/attachment/' + responseData.sharedUrl);
            setShowModal(true);
        } catch (error) {
            console.error('Error sending attachment:', error);
        }
    };



    const copyLinkToClipboard = () => {
        navigator.clipboard.writeText(generatedLink);
    };

    return (
        <div className="button-dropdown-container">
            <ButtonGroup className="custom-button-group">
                <Button variant="secondary" size="sm">
                    Share Link
                </Button>
                <Dropdown onSelect={handleDropdownSelect} className="custom-dropdown">
                    <Dropdown.Toggle split variant="secondary" size="sm" id="dropdown-split-basic" className="custom-toggle" />
                    <Dropdown.Menu className="custom-dropdown-menu">
                        <Dropdown.Item eventKey="1h">Valid 1 Hour</Dropdown.Item>
                        <Dropdown.Item eventKey="3h">Valid 3 Hours</Dropdown.Item>
                        <Dropdown.Item eventKey="1d">Valid 1 Day</Dropdown.Item>
                        <Dropdown.Item eventKey="3d">Valid 3 Days</Dropdown.Item>
                    </Dropdown.Menu>
                </Dropdown>
            </ButtonGroup>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Generated Link</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>{generatedLink}</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>Cancel</Button>
                    <Button variant="primary" onClick={copyLinkToClipboard}>Copy Link</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default ShareDropdown;
