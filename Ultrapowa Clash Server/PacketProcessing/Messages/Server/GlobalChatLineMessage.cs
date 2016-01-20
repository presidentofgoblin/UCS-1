﻿using System;
using System.Collections.Generic;
using UCS.Logic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    //Packet 24715
    class GlobalChatLineMessage : Message
    {
        private string m_vMessage;
        private long m_vHomeId;
        private long m_vCurrentHomeId;
        private string m_vPlayerName;
        private int m_vLeagueId;
        private bool m_vHasAlliance;
        private int m_vAllianceIcon;
        private string m_vAllianceName;
        private long m_vAllianceId;
        private int m_vPlayerLevel;


        public GlobalChatLineMessage(Client client) : base(client)
        {
            SetMessageType(24715);

            m_vMessage = "default";
            m_vPlayerName = "default";
            m_vHomeId = 1;
            m_vCurrentHomeId = 1;
            m_vPlayerLevel = 1;
            m_vHasAlliance = false;
        }

        public override void Encode()
        {
            List<Byte> pack = new List<Byte>();

            pack.AddString(m_vMessage);
            pack.AddString(m_vPlayerName);
            pack.AddInt32(m_vPlayerLevel);
            pack.AddInt32(m_vLeagueId);
            pack.AddInt64(m_vHomeId);
            pack.AddInt64(m_vCurrentHomeId);

            if (!m_vHasAlliance)
            {
                pack.Add((Byte)0);
            }
            else
            {
                pack.Add((Byte)1);
                pack.AddInt64(m_vAllianceId);
                pack.AddString(m_vAllianceName);
                pack.AddInt32(m_vAllianceIcon);
            }

            SetData(pack.ToArray());
        }

        public void SetChatMessage(string message)
        {
            m_vMessage = message;
        }

        public void SetPlayerId(long id)
        {
            m_vHomeId = id;
            m_vCurrentHomeId = id;
        }

        public void SetPlayerName(string name)
        {
            m_vPlayerName = name;
        }

        public void SetLeagueId(int leagueId)
        {
            m_vLeagueId = leagueId;
        }

        public void SetAlliance(Alliance alliance)
        {
            if (alliance != null)
            {
                m_vHasAlliance = true;
                m_vAllianceId = alliance.GetAllianceId();
                m_vAllianceName = alliance.GetAllianceName();
                m_vAllianceIcon = alliance.GetAllianceBadgeData();
            }
        }
    }
}
