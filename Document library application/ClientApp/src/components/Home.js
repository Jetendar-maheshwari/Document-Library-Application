import React, { Component } from "react";
import DocumentUpload from "../components/DocumentUpload/DocumentUpload";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
            <h2>Welcome to our File Upload Library System</h2>
            <DocumentUpload />
      </div>
    );
  }
}
