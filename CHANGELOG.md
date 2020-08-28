# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### [1.0.0] - 2020-8-27

## Added

- Analytics screen hit objects (`AnalyticsScreenHit`) designed to send screen [hits](https://support.google.com/analytics/answer/6086082?hl=en).
- Analytics event hit objects (`AnalyticsEventHit`) designed to send event [hits](https://support.google.com/analytics/answer/1033068?hl=en)
- Specialized dispatcher components for each: `AnalyticsScreenHitDispatcher` and `AnalyticsEventHitDispatcher`, respectively.
- UI Sample scene showing a simple UI analytics demo.