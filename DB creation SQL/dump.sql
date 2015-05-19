--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;

SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: countries; Type: TABLE; Schema: public; Owner: enrico; Tablespace: 
--

CREATE TABLE countries (
    idcountry character varying(4) NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE countries OWNER TO enrico;

--
-- Name: countrylanguage_id_seq; Type: SEQUENCE; Schema: public; Owner: enrico
--

CREATE SEQUENCE countrylanguage_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE countrylanguage_id_seq OWNER TO enrico;

--
-- Name: country_language; Type: TABLE; Schema: public; Owner: enrico; Tablespace: 
--

CREATE TABLE country_language (
    id integer DEFAULT nextval('countrylanguage_id_seq'::regclass) NOT NULL,
    month character varying(20) NOT NULL,
    n_tweet integer,
    country_fk character varying(4) NOT NULL,
    language_fk integer NOT NULL
);


ALTER TABLE country_language OWNER TO enrico;

--
-- Name: languagetweet_id_seq; Type: SEQUENCE; Schema: public; Owner: enrico
--

CREATE SEQUENCE languagetweet_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE languagetweet_id_seq OWNER TO enrico;

--
-- Name: language_tweet; Type: TABLE; Schema: public; Owner: enrico; Tablespace: 
--

CREATE TABLE language_tweet (
    id integer DEFAULT nextval('languagetweet_id_seq'::regclass) NOT NULL,
    language_fk integer NOT NULL,
    tweet_fk character varying(50) NOT NULL
);


ALTER TABLE language_tweet OWNER TO enrico;

--
-- Name: languages; Type: TABLE; Schema: public; Owner: enrico; Tablespace: 
--

CREATE TABLE languages (
    idlanguage integer NOT NULL,
    name character varying(20) NOT NULL
);


ALTER TABLE languages OWNER TO enrico;

--
-- Name: tweets; Type: TABLE; Schema: public; Owner: enrico; Tablespace: 
--

CREATE TABLE tweets (
    idtweet character varying(50) NOT NULL,
    country character varying(50) NOT NULL,
    text character varying(140) NOT NULL,
    creationdate date NOT NULL
);


ALTER TABLE tweets OWNER TO enrico;

--
-- Data for Name: countries; Type: TABLE DATA; Schema: public; Owner: enrico
--



--
-- Data for Name: country_language; Type: TABLE DATA; Schema: public; Owner: enrico
--



--
-- Name: countrylanguage_id_seq; Type: SEQUENCE SET; Schema: public; Owner: enrico
--

SELECT pg_catalog.setval('countrylanguage_id_seq', 1, false);


--
-- Data for Name: language_tweet; Type: TABLE DATA; Schema: public; Owner: enrico
--



--
-- Data for Name: languages; Type: TABLE DATA; Schema: public; Owner: enrico
--



--
-- Name: languagetweet_id_seq; Type: SEQUENCE SET; Schema: public; Owner: enrico
--

SELECT pg_catalog.setval('languagetweet_id_seq', 1, false);


--
-- Data for Name: tweets; Type: TABLE DATA; Schema: public; Owner: enrico
--



--
-- Name: countries_pkey; Type: CONSTRAINT; Schema: public; Owner: enrico; Tablespace: 
--

ALTER TABLE ONLY countries
    ADD CONSTRAINT countries_pkey PRIMARY KEY (idcountry);


--
-- Name: country_language_pkey; Type: CONSTRAINT; Schema: public; Owner: enrico; Tablespace: 
--

ALTER TABLE ONLY country_language
    ADD CONSTRAINT country_language_pkey PRIMARY KEY (id);


--
-- Name: language_tweet_pkey; Type: CONSTRAINT; Schema: public; Owner: enrico; Tablespace: 
--

ALTER TABLE ONLY language_tweet
    ADD CONSTRAINT language_tweet_pkey PRIMARY KEY (id);


--
-- Name: languages_pkey; Type: CONSTRAINT; Schema: public; Owner: enrico; Tablespace: 
--

ALTER TABLE ONLY languages
    ADD CONSTRAINT languages_pkey PRIMARY KEY (idlanguage);


--
-- Name: tweets_pkey; Type: CONSTRAINT; Schema: public; Owner: enrico; Tablespace: 
--

ALTER TABLE ONLY tweets
    ADD CONSTRAINT tweets_pkey PRIMARY KEY (idtweet);


--
-- Name: country_language_country_fk_fkey; Type: FK CONSTRAINT; Schema: public; Owner: enrico
--

ALTER TABLE ONLY country_language
    ADD CONSTRAINT country_language_country_fk_fkey FOREIGN KEY (country_fk) REFERENCES countries(idcountry) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: country_language_language_fk_fkey; Type: FK CONSTRAINT; Schema: public; Owner: enrico
--

ALTER TABLE ONLY country_language
    ADD CONSTRAINT country_language_language_fk_fkey FOREIGN KEY (language_fk) REFERENCES languages(idlanguage) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: language_tweet_language_fk_fkey; Type: FK CONSTRAINT; Schema: public; Owner: enrico
--

ALTER TABLE ONLY language_tweet
    ADD CONSTRAINT language_tweet_language_fk_fkey FOREIGN KEY (language_fk) REFERENCES languages(idlanguage) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: language_tweet_tweet_fk_fkey; Type: FK CONSTRAINT; Schema: public; Owner: enrico
--

ALTER TABLE ONLY language_tweet
    ADD CONSTRAINT language_tweet_tweet_fk_fkey FOREIGN KEY (tweet_fk) REFERENCES tweets(idtweet) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

