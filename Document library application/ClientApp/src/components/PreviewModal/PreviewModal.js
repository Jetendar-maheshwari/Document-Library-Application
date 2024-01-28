import React, { useEffect, useState } from 'react';
import { Modal } from 'react-bootstrap';
import { Document, Page, pdfjs } from 'react-pdf';
import 'react-pdf/dist/esm/Page/AnnotationLayer.css';

const PreviewModal = ({ fileUrl, closeModal }) => {
    const [numPages, setNumPages] = useState(null);
    const [pageNumber, setPageNumber] = useState(1);
    const [isPDF, setIsPDF] = useState(false);
    const [imageSrc, setImageSrc] = useState(null);
    const [pdfSource, setPdfSource] = useState(null);

    useEffect(() => {
        debugger;
        if (fileUrl.toLowerCase().endsWith('.pdf')) {
            setIsPDF(true);
            setPdfSource(process.env.PUBLIC_URL + '/attachment/preview/' + fileUrl);
        } else {
            setIsPDF(false);
            setImageSrc(process.env.PUBLIC_URL + '/attachment/preview/' + fileUrl);
        }
    }, [fileUrl]);


    const onDocumentLoadSuccess = ({ numPages }) => {
        setNumPages(numPages);
    };

    return (
        <Modal show={true} onHide={closeModal} size="lg" centered>
            <Modal.Header closeButton>
                <Modal.Title style={{ maxWidth: '40px', maxHeight: '40px' }}>Preview</Modal.Title>


            </Modal.Header>
            <Modal.Body>
                {isPDF ? (
                    <iframe
                        title="PDF Preview"
                        src={pdfSource}
                        width="100%"
                        height="500px"
                    ></iframe>
                ) : (
                    imageSrc && (
                        <img
                            src={imageSrc}
                            alt="Preview"
                            style={{ maxWidth: '100%', maxHeight: '80vh' }}
                        />
                    )
                )}

            </Modal.Body>
            <Modal.Footer>
                <button className="btn btn-secondary" onClick={closeModal}>
                    Close
                </button>
            </Modal.Footer>
        </Modal>
    );
};

export default PreviewModal;

