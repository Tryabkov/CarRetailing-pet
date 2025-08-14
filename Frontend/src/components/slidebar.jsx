import React from "react";
import "./Sidebar.css";

export default function Sidebar({ setActiveTab, activeTab }) {
  return (
    <aside className="sidebar">
      <div className="profile-info">
        <img
          src="https://via.placeholder.com/120"
          alt="avatar"
          className="avatar"
        />
        <h2 className="username">Your Name</h2>
        <p className="subtitle">Some description</p>
      </div>
      <div className="buttons">
        <button
          className={`sidebar-btn ${activeTab === "profile" ? "active" : ""}`}
          onClick={() => setActiveTab("profile")}
        >
          Edit Profile
        </button>
        <button
          className={`sidebar-btn ${activeTab === "ads" ? "active" : ""}`}
          onClick={() => setActiveTab("ads")}
        >
          My Ads
        </button>
      </div>
    </aside>
  );
}
