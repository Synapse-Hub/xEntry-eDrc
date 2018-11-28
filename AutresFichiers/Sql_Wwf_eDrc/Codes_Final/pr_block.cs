  case 5:
                                        #region fiche Plantation a Realiser v2
                                        //Main row
                                        DataRow myrow93 = dtFichePr.NewRow();
                                        myrow93["uuid"] = instanceId; //The name of the directory is the name of the instance

                                        foreach (DataColumn col in dtFichePr.Columns)
                                        {
                                            XmlNodeList nodeList = doc.GetElementsByTagName(col.ColumnName);
                                            //if (nodeList != null && nodeList.Count == 1)
                                            #region detail
                                            switch (col.ColumnName)
                                            {
                                                #region Autres Essences Melangees
                                                case "rpt_b":
                                                    foreach (XmlNode idp in doc.GetElementsByTagName(col.ColumnName))//doc.DocumentElement.ChildNodes)
                                                    {
                                                        switch (idp.Name)
                                                        {
                                                            case "rpt_b":
                                                                DataRow rowX = null;
                                                                rowX = dtAutresEssencesMelangeesFichePr.NewRow();
                                                                rowX["uuid"] = instanceId; // a creer dans la table
                                                                foreach (XmlNode idp_enfant in idp.ChildNodes)
                                                                {
                                                                    switch (idp_enfant.Name)
                                                                    {
                                                                        case "autre_essence":
                                                                            rowX["autre_essence"] = idp_enfant.InnerText;
                                                                            break;
                                                                        case "autre_essence_autre":
                                                                            rowX["autre_essence_autre"] = idp_enfant.InnerText;
                                                                            break;
                                                                        case "autre_essence_pourcentage":
                                                                            //if (idp_enfant.InnerText != "")
                                                                            rowX["autre_essence_pourcentage"] = int.Parse(idp_enfant.InnerText);
                                                                            break;
                                                                        case "autre_essence_count":
                                                                            rowX["autre_essence_count"] = idp_enfant.InnerText;
                                                                            break;
                                                                        case "synchronized_on":
                                                                            rowX["synchronized_on"] = idp_enfant.InnerText;
                                                                            break;
                                                                        //case "capacite_totale_planche":
                                                                        //    if (idp_enfant.InnerText != "")
                                                                        //        rowA["capacite_totale_planche"] = int.Parse(idp_enfant.InnerText);
                                                                        //    break;
                                                                    }
                                                                }
                                                                dtAutresEssencesMelangeesFichePr.Rows.Add(rowX);
                                                                break;
                                                        }

                                                    }

                                                    break;
                                                #endregion

                                                #region rpt_Arbres
                                                case "rpt_c":
                                                    foreach (XmlNode idp in doc.GetElementsByTagName(col.ColumnName))//doc.DocumentElement.ChildNodes)
                                                    {
                                                        switch (idp.Name)
                                                        {
                                                            case "rpt_c":
                                                                DataRow rowX = null;
                                                                rowX = dtArbresFichePr.NewRow();
                                                                rowX["uuid"] = instanceId; // a creer dans la table
                                                                foreach (XmlNode idp_enfant in idp.ChildNodes)
                                                                {
                                                                    switch (idp_enfant.Name)
                                                                    {
                                                                        case "hauteur_total":
                                                                            rowX["hauteur_total"] = idp_enfant.InnerText;
                                                                            break;
                                                                        case "hauteur_tronc":
                                                                            rowX["hauteur_tronc"] = idp_enfant.InnerText;
                                                                            break;
                                                                        case "houppier_1":
                                                                           // if (idp_enfant.InnerText != "")
                                                                            rowX["houppier_1"] = int.Parse(idp_enfant.InnerText);
                                                                            break;
                                                                        case "houppier_2":
                                                                           // if (idp_enfant.InnerText != "")
                                                                            rowX["houppier_2"] = int.Parse(idp_enfant.InnerText);
                                                                            break;
                                                                        case "diametre":
                                                                            rowX["diametre"] = idp_enfant.InnerText;
                                                                            break;
                                                                    }
                                                                }
                                                                dtArbresFichePr.Rows.Add(rowX);
                                                                break;
                                                        }

                                                    }

                                                    break;
                                                #endregion


                                                default:
                                                    if (nodeList != null && nodeList.Count == 1)
                                                        setValue(nodeList[0].InnerText, myrow93, col.ColumnName);
                                                    //if (col.ColumnName == "created_by") currentUser = nodeList[0].InnerText;
                                                    break;
                                            }
                                            #endregion
                                        }
                                        dtFichePr.Rows.Add(myrow93);

                                        #endregion
                                        break;